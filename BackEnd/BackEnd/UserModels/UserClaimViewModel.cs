using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.UserModels
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            Claims = new List<UserClaim>();
        }
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
