using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.UserModels
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Booking","Booking"),
            new Claim("Area","Area"),
            new Claim("Ticket","Ticket"),
            new Claim("Activity","Activity"),
            new Claim("Users","Users")  ,
            new Claim("Event" , "Event")
        };
    }
}
