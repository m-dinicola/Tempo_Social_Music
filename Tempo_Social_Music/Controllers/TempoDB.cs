using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tempo_Social_Music.Models;

namespace Tempo_Social_Music.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TempoDB : ControllerBase
    {
        private Tempo_DBContext _context;

        public TempoDB(Tempo_DBContext context)
        {
            _context = context;
        }

        #region Create
        //POST: api/TempoDB/user
        //pair progammed by AL & MD
        //allows creation of new Mixee if active user doesn't already have a handle
        [HttpPost("user")]
        public async Task<ActionResult<FrontEndUser>> CreateUser(FrontEndUser newUser)
        {
            string aspNetId;
            try
            {
                aspNetId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (_context.TempoUser.Select(x => x.LoginName).Contains(newUser.LoginName)
                    || _context.TempoUser.FirstOrDefaultAsync(x => x.AspNetUserId == aspNetId).Result != null)
                {
                    return Unauthorized();
                    //loginName already exists or user already has a login name cannot create new user.
                }

                if (string.IsNullOrEmpty(newUser.LoginName) || string.IsNullOrEmpty(newUser.FirstName))
                {
                    return BadRequest();
                }
            }
            catch (NullReferenceException)
            {
                //if user is not logged in, aspNetId produces a NullReferenceException
                return Forbid();
            }

            TempoUser newTempoUser = new TempoUser(newUser, aspNetId); //creates TempoUser with frontenduser and aspNetID string
            _context.TempoUser.Add(newTempoUser);    //add new user to database
            await _context.SaveChangesAsync();  //save changes to database
            return CreatedAtAction(nameof(GetUserByName), new { username = newUser.LoginName }, new FrontEndUser(newTempoUser));
            //allow redirect to user page for new user

        }

        //POST: api/TempoDB/addUserFriend
        //pair progreammed by AL & MD
        //posts a new connection to the connection table between active user and userString Mixee
        //note: connections are symmetric, which means either party can initiate or end a connection
        //potential for harrassment or abuse. Will eventually change symmetry
        [HttpPost("addUserFriend/{userString}")] //user string is of the Mixee that active user wants to connect with
        public async Task<ActionResult<List<FrontEndConnection>>> AddConnection(string userString)
        {
            FrontEndUser user1 = await ActiveFrontEndUser();
            Connection newConnection;
            try
            {
                newConnection = ParseConnectionUserString(user1.LoginName, userString);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }
            //see if table already contains match for these users
            Connection oldConnection = await PreexistingConnectionAsync(newConnection);

            //add new connection to DB if it doesn't already exist, return Created ActionResult
            if (oldConnection is null)
            {
                _context.Connection.Add(newConnection);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetConnectionsAsync), new { userPK = newConnection.User1 }, new FrontEndConnection(newConnection));
            }

            //if connection already exists, just return OK actionresult.
            return Ok(new FrontEndConnection(oldConnection));
        }

        // POST: api/tempoDB/Jams
        // by M
        [HttpPost("Jams")]
        public async Task<ActionResult<FrontEndFavorite>> AddJam(FrontEndFavorite newFave)
        {
            //add the active user to the desired new Jam
            newFave.UserId = ActiveFrontEndUser().Result.UserPk;
            //check if that user already has that as a Jam
            Favorites oldFave = PreexistingFavoriteAsync(new Favorites(newFave)).Result;
            if (oldFave != null)
            {
                //if so, no change necessary
                return Ok(newFave);
            }

            if (!ModelState.IsValid)
            {
                //if the model state doesn't work, don't add it to the DB
                return BadRequest();
            }

            _context.Favorites.Add(new Favorites(newFave)); //if all tests pass, add to the DB
            await _context.SaveChangesAsync();              //save changes
            return CreatedAtAction(nameof(GetFavoritesAsync), new { userPK = newFave.UserId }, newFave);
            //allow reroute to favorites list.
        }


        #endregion
        #region Read
        // GET: api/TempoDB/username
        [HttpGet("username/{username}")]
        public async Task<ActionResult<FrontEndUser>> GetUserByName(string username)
        {
            var getUser = await _context.TempoUser.FirstAsync(x => x.LoginName.ToLower() == username.ToLower());
            if (getUser is null)
            {
                return NotFound();
            }
            return new FrontEndUser(getUser);
        }

        // GET: api/TempoDB/userID
        [HttpGet("userID/{userID}")]
        public ActionResult<FrontEndUser> GetUser(int userID)
        {
            var getUser = _context.TempoUser.FirstOrDefaultAsync(x => x.UserPk == userID).Result;
            if (getUser is null)
            {
                return NotFound();
            }
            return Ok(new FrontEndUser(getUser));
        }

        // GET: api/TempoDB/Jams/{userPK}
        // by M
        [HttpGet("Jams/{userPK}")]
        public async Task<List<FrontEndFavorite>> GetFavoritesAsync(int userPK)
        {
            var awaiter = await _context.Favorites.Where(x => x.UserId == userPK).ToListAsync();
            var output = new List<FrontEndFavorite>();
            awaiter.ForEach(x => output.Add(new FrontEndFavorite(x)));
            return output;
        }

        // GET: api/TempoDB/Connections/{userPK}
        // pair programmed by M & AL
        [HttpGet("connections/{userPK}")]
        public async Task<List<FrontEndConnection>> GetConnectionsAsync(int userPK)
        {
            //return filtered table of connections which have userPK as User1 or User2 from tempoDB.Connection table
            var awaiter = await _context.Connection.Where(x => x.User1 == userPK || x.User2 == userPK).OrderByDescending(x => x.MatchValue).ToListAsync();
            var output = new List<FrontEndConnection>();
            awaiter.ForEach(x => output.Add(new FrontEndConnection(x)));
            return output;
        }

        //GET: api/TempoDB/user  
        [HttpGet("user")]
        public async Task<ActionResult<FrontEndUser>> GetCurrentUser()
        {
            //returns active tempoDB user parsed as front end user object
            FrontEndUser foundUser = await ActiveFrontEndUser();
            if (foundUser is null)
            {
                return Unauthorized();
            }
            return Ok(foundUser);
        }


        #endregion
        #region Update

        //PUT: api/TempoDB/user
        //pair progammed by MH & MD
        //allow alteration of certain user values
        [HttpPut("user")]
        public async Task<ActionResult<FrontEndUser>> UpdateUser(FrontEndUser newUser)
        {
            //identify which Mixee is active user
            FrontEndUser activeUser = await ActiveFrontEndUser();
            //grab the old Mixee from DB, if it exist
            TempoUser oldUser = await _context.TempoUser.FindAsync(activeUser.UserPk);
            try
            {
                //old Mixee will be null if active user has no associated Mixr acct.
                if (oldUser is null
                    || (_context.TempoUser.Select(x => x.LoginName).Contains(newUser.LoginName)
                        && newUser.LoginName != oldUser.LoginName))
                {
                    return BadRequest();
                    //if no Mixee exists for a given user, or new username already exists cannot apply new values.
                }
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e);
            }
            //apply any non-null values to old user
            oldUser.LoginName = string.IsNullOrEmpty(newUser.LoginName) ? oldUser.LoginName : newUser.LoginName;
            oldUser.FirstName = string.IsNullOrEmpty(newUser.FirstName) ? oldUser.FirstName : newUser.FirstName;
            oldUser.LastName = string.IsNullOrEmpty(newUser.LastName) ? oldUser.LastName : newUser.LastName;
            oldUser.StreetAddress = string.IsNullOrEmpty(newUser.StreetAddress) ? oldUser.StreetAddress : newUser.StreetAddress;
            oldUser.ZipCode = string.IsNullOrEmpty(newUser.ZipCode) ? oldUser.ZipCode : newUser.ZipCode;
            oldUser.State = string.IsNullOrEmpty(newUser.State) ? oldUser.State : newUser.State;
            oldUser.UserBio = string.IsNullOrEmpty(newUser.UserBio) ? oldUser.UserBio : newUser.UserBio;

            _context.Entry(oldUser).State = EntityState.Modified;    //track modification of old TempoUser
            _context.Update(oldUser);
            await _context.SaveChangesAsync();  //save changes to database
            return AcceptedAtAction(nameof(GetUserByName), new { username = oldUser.LoginName }, new FrontEndUser(oldUser));
            //allow redirect to user page for new user
        }

        #endregion
        #region Delete
        //DELETE: api/tempoDB/deleteUserFriend/{userString}
        //pair programmed by M and AL
        //deletes connection between a given user and active user
        [HttpDelete("deleteUserFriend/{userString}")]
        public async Task<ActionResult> DeleteConnection(string userString)
        {
            //gets active user
            FrontEndUser user1 = await ActiveFrontEndUser();
            Connection connection;
            try
            {
                //uses legacy support function to make a connection from two usernames
                connection = ParseConnectionUserString(user1.LoginName, userString);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }

            //sees if connection already exists
            Connection oldConnection = await PreexistingConnectionAsync(connection);
            if (oldConnection is null)
            {
                //if it doesn't, no need to delete
                return Ok();
            }

            //remove the connection from DB
            _context.Connection.Remove(oldConnection);
            await _context.SaveChangesAsync();
            //no content expected upon successful delete, code 204
            return NoContent();

        }

        //DELETE: api/tempoDB/Jams/
        //by M
        //deletes a Jam
        [HttpDelete("Jams/{jam}")]
        public async Task<ActionResult> DeleteJamAsync(int jam) //jam is the primary key for the favorites table
        {
            //gets the jam with a given key
            Favorites oldJam = await _context.Favorites.FindAsync(jam);
            if (oldJam is null)
            {
                // if the jam doesn't exist, no need to delete
                return Ok();
            }

            //tests that current Mixee is the owner of the jam
            if (oldJam.UserId == ActiveFrontEndUser().Result.UserPk)
            {
                _context.Favorites.Remove(oldJam);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            //if tests fail, return unauthorized. Active user might not be Jam owner, or other edge case.
            return Unauthorized();
        }

        #endregion
        #region Support fxns
        private Connection ParseConnectionUserString(string user1, string user2)
        {
            //parse userString into usernames
            List<string> users = new List<string>
            {
                user1,
                user2
            };

            //test to ensure 2 users were passed
            if (users.Count != 2 || users.Intersect(_context.TempoUser.Select(x => x.LoginName)).Count() != 2)
            {
                throw new IndexOutOfRangeException();
            }

            //build a connection from two usernames
            Connection newConnection = new Connection
            {
                MatchValue = 1,
                //search table of users for user with username to extract userPK
                User1 = _context.TempoUser.FirstAsync(x => x.LoginName == users[0]).Result.UserPk,
                User2 = _context.TempoUser.FirstAsync(x => x.LoginName == users[1]).Result.UserPk
            };

            return newConnection;
        }

        private async Task<Connection> PreexistingConnectionAsync(Connection connection)
        {
            //see if table already contains match for these users
            Connection oldConnection = await _context.Connection.FirstOrDefaultAsync(x =>
                (x.User1 == connection.User1 && x.User2 == connection.User2) ||
                (x.User1 == connection.User2 && x.User2 == connection.User1)
                );
            return oldConnection;
        }

        private async Task<Favorites> PreexistingFavoriteAsync(Favorites favorite)
        {
            //see if table already contains match for these users
            Favorites oldFavorite = await _context.Favorites.FirstOrDefaultAsync(x =>
                (
                x.UserId == favorite.UserId
                && (x.SpotArtist == favorite.SpotArtist && x.SpotTrack == favorite.SpotTrack))
                );
            return oldFavorite;
        }

        private async Task<FrontEndUser> ActiveFrontEndUser()
        {
            var aspNetId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TempoUser foundUser = await _context.TempoUser.FirstOrDefaultAsync(x => x.AspNetUserId == aspNetId);
            return new FrontEndUser(foundUser);

        }
        #endregion
    }
}
