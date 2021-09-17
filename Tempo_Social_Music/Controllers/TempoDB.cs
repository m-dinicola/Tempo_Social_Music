﻿using Microsoft.AspNetCore.Http;
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
        //POST: api/TempoDB/user
        //pair progammed by AL & MD
        [HttpPost("user")]
        public async Task<ActionResult<TempoUser>> CreateUser(TempoUser newUser)
        {
            if (newUser.LoginName is null || GetUserByName(newUser.LoginName) != null || newUser.FirstName is null)
            {
                return BadRequest();
                //if required fields are empty or username already exists cannot create new user.
            }
            _context.TempoUser.Add(newUser);    //add new user to database
            await _context.SaveChangesAsync();  //save changes to database
            return CreatedAtAction(nameof(GetUserByName), new { username = newUser.LoginName}, newUser);  
            //redirect to user page for new user

        }

        //POST: api/TempoDB/addUserFriend
        //pair progreammed by AL & MD
        [HttpPost ("addUserFriend/{userString}")]
        public async Task<ActionResult<TempoUser>> AddConnection(string userString)
        {
            List<string> users = userString.Split('&').ToList();
            return NotFound();
        }
        #endregion
    }
}
