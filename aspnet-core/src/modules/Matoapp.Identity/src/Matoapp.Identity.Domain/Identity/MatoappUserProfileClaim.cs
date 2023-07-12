using System;
using System.Collections.Generic;
using System.Text;

namespace Matoapp.Identity.Identity
{
    public class MatoappUserProfileClaim
    {
        public MatoappUserProfileClaim()
        {
            
        }
        public string[] Permission { get; set; }
        public string[] Role { get; set; }

        public string AvatarUrl { get; set; }
        public string BirthDay { get; set; }


    }
}
