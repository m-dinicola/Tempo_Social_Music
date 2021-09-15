using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tempo_Social_Music.Models
{
    public partial class Connection
    {
        public int ConnectionId { get; set; }
        public int MatchValue { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
    }
}
