using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempo_Social_Music.Models;

namespace Tempo_Social_Music.Controllers
{
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
        public async Task<ActionResult<TempoUser>> GetUserByName(string username) {
            var getUser = await _context.TempoUser.FirstAsync(x => x.LoginName.ToLower() == username.ToLower());
            if (getUser is null)
            {
                return NotFound();
            }
            return getUser;
        }

        // GET: api/TempoDB/userID
        //[HttpGet("userID/{id}")]
        //public async Task<ActionResult<TempoUser>> GetUser(int userID) {
        //    var getUser = await _context.TempoUser.FirstOrDefaultAsync(x => x.UserPk == userID);
        //    if(getUser is null)
        //    {
        //        return NotFound();
        //    }
        //    return getUser;
        //}

        // GET: api/TempoDB/Connections/{userPK}
        // pair programmed by M & AL
        [HttpGet("connections/{userPK}")]
        public List<Connection> GetConnections(int userPK)
        {
            //return filtered table of connections which have userPK as User1 or User2 from tempoDB.Connection table
            return _context.Connection.Where(x => x.User1 == userPK || x.User2 == userPK).ToList();
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
        public async Task<ActionResult<List<Connection>>> AddConnection(string userString)
        {
            Connection newConnection;
            try
            {
                newConnection = ParseConnectionUserString(userString);
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
                return CreatedAtAction(nameof(GetConnections), new { userPK = newConnection.User1}, newConnection);
            }

            //if connection already exists, just return OK actionresult.
            return Ok(oldConnection);
        }

        #endregion
        #region Delete
        //DELETE: api/tempoDB/deleteUserFriend/{userString}
        //pair programmed by M and AL
        [HttpDelete("deleteUserFriend/{userString}")]
        public async Task<ActionResult<List<Connection>>> deleteConnection(string userString)
        {
            Connection connection;
            try
            {
                connection = ParseConnectionUserString(userString);
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

        private Connection ParseConnectionUserString(string userString)
        {
            //parse userString into usernames
            List<string> users = userString.Split('&').ToList();

            //test to ensure 2 users were passed
            if (users.Count != 2 || _context.TempoUser.Select(x => x.LoginName).Intersect(users).Count() != 2)
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

        #endregion
    }
}
