using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempo_Social_Music.Models
{
    public class FrontEndConnection
    {

        public FrontEndConnection(Connection connection)
        {
            ConnectionId = connection.ConnectionId;
            MatchValue = connection.MatchValue;
            User1 = connection.User1;
            User2 = connection.User2;
        }

        public int ConnectionId { get; set; }
        public int MatchValue { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
    }
}
