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
            if(getUser is null)
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

        #endregion
        #region Create
        //POST: api/TempoDB/username
        [HttpPost("user")]
        public async Task<ActionResult<TempoUser>> CreateUser(TempoUser newUser)
        {
            if (GetUserByName(newUser.LoginName) != null)
            {
                return BadRequest();
            }
            return NotFound();
        }
               #endregion
    }
}
