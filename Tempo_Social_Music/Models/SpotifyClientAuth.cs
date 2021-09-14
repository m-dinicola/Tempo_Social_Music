using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tempo_Social_Music.Models
{
    public class SpotifyClientAuth
    {
        private readonly SpotifyConfig _keys;

        private BearerToken _token;

        public DateTime ValidTo { get; set; }
        public SpotifyClientAuth(IConfiguration config)
        {
            _keys = config.GetSection("Spotify").Get<SpotifyConfig>();
        }

        private async Task SetTokenAsync()
        {
            string uriEndpoint = "https://accounts.spotify.com/api/token";
            string authorizationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(_keys.ClientID + ":" + _keys.ClientSecret));

            List<KeyValuePair<string, string>> arguments = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type","client_credentials")
            };

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Basic {authorizationString}");
            HttpContent content = new FormUrlEncodedContent(arguments);
            HttpResponseMessage response = await http.PostAsync(uriEndpoint, content);
            string responseString = await response.Content.ReadAsStringAsync();
            _token = JsonSerializer.Deserialize<BearerToken>(responseString);
            ValidTo = DateTime.UtcNow.AddSeconds(_token.expires_in);
        }

        public async Task<BearerToken> GetTokenAsync()
        {
            if (IsExpiredOrEmpty())
            {
                await SetTokenAsync();
            }
            return _token;
        }

        private bool IsExpiredOrEmpty()
        {
            return (string.IsNullOrEmpty(_token.access_token)) || (DateTime.UtcNow > ValidTo) || (_token.token_type != "bearer");
        }
    }
}
