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
        [HttpPost("addUserFriend/{userString}")] //Commented out due to build error. Needs to be fixed please.
        public async Task<ActionResult<Connection>> AddConnection(string userString)
        {
            List<string> users = userString.Split('&').ToList();
            if (users.Count != 2 || _context.TempoUser.Select(x => x.LoginName).Intersect(users).Count() != 2)
            {
                return BadRequest();
            }
            
            Connection newConnection = new Connection();
            newConnection.MatchValue = 1;
            newConnection.User1 = _context.TempoUser.FirstAsync(x => x.LoginName == users[0]).Result.UserPk;
            newConnection.User2 = _context.TempoUser.FirstAsync(x => x.LoginName == users[1]).Result.UserPk;

            Connection oldConnection = await _context.Connection.FirstOrDefaultAsync(x =>
                (x.User1 == newConnection.User1 && x.User2 == newConnection.User2) ||
                (x.User1 == newConnection.User2 && x.User2 == newConnection.User1)
                );
            if (oldConnection is null)
            {
                _context.Connection.Add(newConnection);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetConnections), new { userPK = newConnection.User1}, newConnection);
            }
            return Ok(oldConnection);
        }

        #endregion
        #region Delete
        //DELETE: api/tempoDB/deleteUserFriend/{userString}
        //pair programmed by M and AL


        #endregion
    }
}
