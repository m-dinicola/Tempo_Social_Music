using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tempo_Social_Music.Models;

namespace Tempo_Social_Music.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private SpotifyClientAuth _token;
        private readonly SpotifyDAL _DAL;
        public SpotifyController(SpotifyClientAuth spotifyClientAuth)
        {
            _token = spotifyClientAuth;
            _DAL = new SpotifyDAL();

        }

        //GET: /getSongByName/{songName}
        //by M
        [HttpGet("getSongByName/{songName}")]
        public async Task<ActionResult<string>> GetSongByName(string songName)
        {
            string query = "?q=" + songName.Replace(" ", "%20") + "&type=track";
            HttpClient http = _DAL.GetHttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.GetTokenAsync().Result.access_token}");
            var request = await http.GetAsync($"search{query}");
            var result = await request.Content.ReadAsAsync<SpotifySearchResult>();
            return JsonSerializer.Serialize(result.tracks.items[0]);
        }

        //GET: /getArtistByName/{artistName}
        //pair programmed by M and AL
        [HttpGet("getArtistByName/{artistName}")]
        public async Task<ActionResult<string>> GetArtistByName(string artistName)
        {
            string query = "?q=" + artistName + "&type=artist";
            HttpClient http = _DAL.GetHttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.GetTokenAsync().Result.access_token}");
            var request = await http.GetAsync($"search{query}");
            var result = await request.Content.ReadAsAsync<SpotifySearchResult>();
            return JsonSerializer.Serialize(result.artists.items[0]);
        }

        //GET: /getsongbyID/{songId}
        //pair programmed AL and M
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
        //by AL
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
