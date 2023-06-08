using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost , Route("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null)
                return BadRequest();
            if(user.UserName == "ahmed" && user.Password == "ahmed@123")
            {
                var secretyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minimumSixteenCharacters"));
                var signingCredentials = new SigningCredentials(secretyKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44328",
                    audience: "https://localhost:44328",
                    claims: new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role , "Manager")
                    },

                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signingCredentials

                    ) ;
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = tokenString });
            }
            return Unauthorized();
        }

    }
}
