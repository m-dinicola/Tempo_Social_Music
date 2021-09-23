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

        #region Read
        // GET: api/TempoDB/username
        [HttpGet("username/{username}")]
        public async Task<ActionResult<FrontEndUser>> GetUserByName(string username) {
            var getUser = await _context.TempoUser.FirstAsync(x => x.LoginName.ToLower() == username.ToLower());
            if (getUser is null)
            {
                return NotFound();
            }
            return new FrontEndUser(getUser);
        }

        // GET: api/TempoDB/userID
        [HttpGet("userID/{userID}")]
        public ActionResult<TempoUser> GetUser(int userID)
        {
            var getUser = _context.TempoUser.FirstOrDefaultAsync(x => x.UserPk == userID).Result;
            if (getUser is null)
            {
                return NotFound();
            }
            return Ok(getUser);
        }

        // GET: api/TempoDB/Jams/{userPK}
        // by M
        [HttpGet("Jams/{userPK}")]
        public async Task<List<FrontEndFavorite>> GetFavoritesAsync(int userPK)
        {
            var awaiter = await _context.Favorites.Where(x => x.UserId == userPK).OrderBy(x => x.SpotArtist).ToListAsync();
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
            var awaiter = await _context.Connection.Where(x => x.User1 == userPK || x.User2 == userPK).OrderByDescending(x=>x.MatchValue).ToListAsync();
            var output = new List<FrontEndConnection>();
            awaiter.ForEach(x => output.Add(new FrontEndConnection(x)));
            return output;
        }

        //GET: api/TempoDB/user  
        [HttpGet("user")]
        public async Task<ActionResult<FrontEndUser>> GetCurrentUser()
        {
            //returns current tempoDB user parsed as front end user object
            FrontEndUser foundUser = await ActiveFrontEndUser();
            if (foundUser is null)
            {
                return Unauthorized();
            }
            return Ok(foundUser);
        }


        #endregion
        #region Create
        //POST: api/TempoDB/user
        //pair progammed by AL & MD
        [HttpPost("user")]
        public async Task<ActionResult<TempoUser>> CreateUser(TempoUser newUser)
        {
            try
            {
                if (_context.TempoUser.Select(x => x.LoginName).Contains(newUser.LoginName) || string.IsNullOrEmpty(newUser.LoginName) || string.IsNullOrEmpty(newUser.FirstName))
                {
                    return BadRequest();
                    //if required fields are empty or username already exists cannot create new user.
                }
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e);
            }
            _context.TempoUser.Add(newUser);    //add new user to database
            await _context.SaveChangesAsync();  //save changes to database
            return CreatedAtAction(nameof(GetUserByName), new { username = newUser.LoginName}, newUser);  
            //redirect to user page for new user

        }

        //POST: api/TempoDB/addUserFriend
        //pair progreammed by AL & MD
        [HttpPost("addUserFriend/{userString}")] //user string is of the form $"{Username1}&{Username2}"
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
                return CreatedAtAction(nameof(GetConnectionsAsync), new { userPK = newConnection.User1}, new FrontEndConnection(newConnection));
            }

            //if connection already exists, just return OK actionresult.
            return Ok(new FrontEndConnection(oldConnection));
        }

        // POST: api/tempoDB/Jams
        // by M
        [HttpPost("Jams")]
        public async Task<ActionResult<Favorites>> AddJam(Favorites newFave)
        {
            Favorites oldFave = PreexistingFavoriteAsync(newFave).Result;
            if(oldFave != null)
            {
                return Ok(new FrontEndFavorite(oldFave));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Favorites.Add(newFave);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFavoritesAsync), new { userPK = newFave.UserId }, new FrontEndFavorite(newFave));
            
        }


        #endregion
        #region Delete
        //DELETE: api/tempoDB/deleteUserFriend/{userString}
        //pair programmed by M and AL
        [HttpDelete("deleteUserFriend/{userString}")]
        public async Task<ActionResult> DeleteConnection(string userString)
        {
            FrontEndUser user1 = await ActiveFrontEndUser();
            Connection connection;
            try
            {
                connection = ParseConnectionUserString(user1.LoginName, userString);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }

            Connection oldConnection = await PreexistingConnectionAsync(connection);
            if(oldConnection is null)
            {
                return Ok();
            }

            _context.Connection.Remove(oldConnection);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        //DELETE: api/tempoDB/Jams/
        //by M
        [HttpDelete("Jams/{jam}")]
        public async Task<ActionResult> DeleteJamAsync(int jam)
        {
            Favorites oldJam = await _context.Favorites.FindAsync(jam);
            if(oldJam is null)
            {
                return Ok();
            }

            _context.Favorites.Remove(oldJam);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        #endregion
        #region Support fxns
        private Connection ParseConnectionUserString(string user1, string user2)
        {
            //parse userString into usernames
            List<string> users = new List<string>();
            users.Add(user1);
            users.Add(user2);

            //test to ensure 2 users were passed
            if (users.Count != 2 || users.Intersect(_context.TempoUser.Select(x => x.LoginName)).Count() != 2)
            {
                throw new IndexOutOfRangeException();
            }

            //build a connection from two usernames
            Connection newConnection = new Connection();
            newConnection.MatchValue = 1;
            //search table of users for user with username to extract userPK
            newConnection.User1 = _context.TempoUser.FirstAsync(x => x.LoginName == users[0]).Result.UserPk;
            newConnection.User2 = _context.TempoUser.FirstAsync(x => x.LoginName == users[1]).Result.UserPk;

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
                (x.UserId == favorite.UserId && x.SpotArtist == favorite.SpotArtist && x.SpotTrack == favorite.SpotTrack)
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
