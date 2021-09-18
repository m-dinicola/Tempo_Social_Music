using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tempo_Social_Music.Models
{
    public class SpotifyDAL
    {
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.spotify.com/v1/");
            return client;
        }
    }
}
