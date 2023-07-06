using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.UserModels
{
    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool Show { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}
