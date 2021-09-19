using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempo_Social_Music.Models
{
    public class FrontEndUser
    {
        public FrontEndUser()
        {
        }

        public FrontEndUser(TempoUser DBUser)
        {
            UserPk = DBUser.UserPk;
            LoginName = DBUser.LoginName;
            FirstName = DBUser.FirstName;
            LastName = DBUser.LastName;
            StreetAddress = DBUser.StreetAddress;
            State = DBUser.State;
            ZipCode = DBUser.ZipCode;
            UserBio = DBUser.UserBio;
        }

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
