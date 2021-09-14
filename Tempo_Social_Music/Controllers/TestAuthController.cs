using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tempo_Social_Music.Models;

namespace Tempo_Social_Music.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        private SpotifyClientAuth _test;
        public TestAuthController(SpotifyClientAuth testClientAuth)
        {
            _test = testClientAuth;
        }

        [HttpGet]
        public string TestAuth()
        {
            BearerToken token;
            token = _test.GetTokenAsync().Result;
            return JsonSerializer.Serialize(token);
        }
    }
}
