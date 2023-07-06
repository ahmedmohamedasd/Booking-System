using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.UserModels
{
    public class EditUserClaimViewModel
    {
        public EditUserClaimViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public string UserName { get; set; }
        public string Id { get; set; }
      
        public List<UserClaim> Claims { get; set; }
    }
}
