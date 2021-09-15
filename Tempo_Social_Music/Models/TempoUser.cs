using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tempo_Social_Music.Models
{
    public partial class TempoUser
    {
        public int UserPk { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserBio { get; set; }
    }
}
