using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tempo_Social_Music.Models
{
    public partial class Favorites
    {
        public int Favorite { get; set; }
        public int UserId { get; set; }
        public string SpotTrack { get; set; }
        public string SpotArtist { get; set; }

        public virtual TempoUser User { get; set; }
    }
}
