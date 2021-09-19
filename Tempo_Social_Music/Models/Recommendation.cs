using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tempo_Social_Music.Models
{
    public partial class Recommendation
    {
        public int RecommendationId { get; set; }
        public int UserFrom { get; set; }
        public int RecSongId { get; set; }
        public int UserTo { get; set; }

        public virtual TempoUser UserFromNavigation { get; set; }
        public virtual TempoUser UserToNavigation { get; set; }
    }
}
