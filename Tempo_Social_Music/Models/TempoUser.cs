using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tempo_Social_Music.Models
{
    public partial class TempoUser
    {
        public TempoUser()
        {
            ConnectionUser1Navigation = new HashSet<Connection>();
            ConnectionUser2Navigation = new HashSet<Connection>();
            Favorites = new HashSet<Favorites>();
            RecommendationUserFromNavigation = new HashSet<Recommendation>();
            RecommendationUserToNavigation = new HashSet<Recommendation>();
        }

        public int UserPk { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserBio { get; set; }
        public string AspNetUserId { get; set; }

        public virtual ICollection<Connection> ConnectionUser1Navigation { get; set; }
        public virtual ICollection<Connection> ConnectionUser2Navigation { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<Recommendation> RecommendationUserFromNavigation { get; set; }
        public virtual ICollection<Recommendation> RecommendationUserToNavigation { get; set; }
    }
}
