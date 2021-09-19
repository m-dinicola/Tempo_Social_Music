using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempo_Social_Music.Models
{
    public class FrontEndFavorite
    {
        public int Favorite { get; set; }
        public int UserId { get; set; }
        public string SpotTrack { get; set; }
        public string SpotArtist { get; set; }

        public FrontEndFavorite(Favorites favorite)
        {
            Favorite = favorite.Favorite;
            UserId = favorite.UserId;
            SpotTrack = favorite.SpotTrack;
            SpotArtist = favorite.SpotArtist;
        }
    }
}
