using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tempo_Social_Music.Models;

namespace Tempo_Social_Music.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private SpotifyClientAuth _token;
        private SpotifyDAL _DAL;
        public SpotifyController(SpotifyClientAuth spotifyClientAuth)
        {
            _token = spotifyClientAuth;
            _DAL = new SpotifyDAL();

        }


        //GET: /getsongbyID/{songId}
        [HttpGet("getsongbyId/{songId}")]
        public async Task<ActionResult<string>> GetSongById(string songId)
        {
            HttpClient http = _DAL.GetHttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.GetTokenAsync().Result.access_token}");

            var request = await http.GetAsync($"tracks/{songId}");
            var response = await request.Content.ReadAsStringAsync();
            return response;
        }

        //GET: /getartistbyID/{artistId}
        [HttpGet("getartistbyId/{artistId}")]
        public async Task<ActionResult<string>> GetArtistById(string artistId)
        {
            HttpClient http = _DAL.GetHttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.GetTokenAsync().Result.access_token}");

            var request = await http.GetAsync($"artists/{artistId}");
            var response = await request.Content.ReadAsStringAsync();
            return response;

        }
    }
}
