using BackEnd.Data;
using BackEnd.Dtos;
using BackEnd.Helpers;
using BackEnd.Models;
using BackEnd.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
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
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpPost , Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            if (user == null)
                return BadRequest();
            var userinDb = _userManager.Users.FirstOrDefault(c => c.NormalizedUserName == user.UserName);
            if(userinDb != null)
            {
                if (await _userManager.CheckPasswordAsync(userinDb, user.Password))
                {
                    var listClaims = await _userManager.GetClaimsAsync(userinDb);
                    var authClaim = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name,user.UserName),
                        //new Claim(ClaimTypes.Role , "manager")
                        };
                    var secretyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:key"]));
                    var signingCredentials = new SigningCredentials(secretyKey, SecurityAlgorithms.HmacSha256);
                    for (int i = 0; i < listClaims.Count; i++)
                    {
                        authClaim.Add(new Claim(listClaims[i].Type, listClaims[i].Value));
                    }
                    var tokenOptions = new JwtSecurityToken(
                        issuer: ConfigurationManager.AppSetting["JWT:Issuer"] ,
                        audience: ConfigurationManager.AppSetting["JWT:Audience"] ,
                        claims: authClaim,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signingCredentials
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return BadRequest("Password Not Match");
                }
            }
            return Unauthorized();
        }
       
        [HttpGet("AddUserClaimViewModel")]
        public ActionResult<UserClaimViewModel> AddUserViewModel()
        {
            var model = new UserClaimViewModel
            {
                Email = "",
                Password = "",
                UserName = ""
            };
            foreach(Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type,
                    Add = false,
                    Delete = false,
                    Edit = false,
                    Show = false
                };
                model.Claims.Add(userClaim);
            }
            return model;
        }
        
        [HttpPost("AddUser")]
        public async Task<ActionResult<UserClaimViewModel>> AddUser(UserClaimViewModel model)
        {
            var existEmail = await _userManager.Users.FirstOrDefaultAsync(c => c.NormalizedEmail ==model.Email);
            
            if(existEmail == null)
            {
                var userName = await _userManager.Users.FirstOrDefaultAsync(c => c.NormalizedUserName == model.UserName);
                if(userName == null)
                {
                    var user = new IdentityUser { Email = model.Email, UserName = model.UserName };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var usrIndb = await _userManager.FindByEmailAsync(model.Email);
                        if (usrIndb != null)
                        {
                            for (int i = 0; i < model.Claims.Count; i++)
                            {
                                var testResult = await _userManager.AddClaimAsync(usrIndb, new Claim(model.Claims[i].ClaimType + "_Show", model.Claims[i].Show ? "true" : "false"));
                                testResult = await _userManager.AddClaimAsync(usrIndb, new Claim(model.Claims[i].ClaimType + "_Add", model.Claims[i].Add ? "true" : "false"));
                                testResult = await _userManager.AddClaimAsync(usrIndb, new Claim(model.Claims[i].ClaimType + "_Edit", model.Claims[i].Edit ? "true" : "false"));
                                testResult = await _userManager.AddClaimAsync(usrIndb, new Claim(model.Claims[i].ClaimType + "_Delete", model.Claims[i].Delete ? "true" : "false"));
                            }
                        }
                        else
                        {
                            return BadRequest("Error in Adding User");
                        }
                    }
                }
                else
                {
                    return BadRequest("UserName exist Change it ");
                }
                
            }
            else
            {
                return NotFound("This Email is Exist");
            }
            return model;
        }

        [HttpGet("ListUsers")]
        public async Task<ActionResult<List<UserDto>>> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserDto> userDtos = new List<UserDto>();
            for(int i= 0; i < users.Count; i++)
            {
                var ss = new UserDto
                {
                    Id = users[i].Id,
                    UserName = users[i].UserName
                };
                userDtos.Add(ss);
            }
            return userDtos;
        }
        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
             if(user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                for(int i = 0; i < claims.Count; i++)
                {
                    await _userManager.RemoveClaimAsync(user,  new Claim(claims[i].Type , claims[i].Value));
                }
                var result =  await  _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                   return Ok();
                }
                else
                {
                    return BadRequest("Error in Deleting User");
                }
            }
            return NotFound();
        }

        [HttpGet("EditUserClaimViewModel/{userId}")]
        public async Task<ActionResult<EditUserClaimViewModel>> EditUserClaimViewModel(string userId)
        {
            var user = await  _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var model = new EditUserClaimViewModel
                {
                    UserName = user.UserName,
                    Id= user.Id
                };
                var userClaims = await _userManager.GetClaimsAsync(user);
                foreach (Claim claim in ClaimsStore.AllClaims)
                {
                    UserClaim userClaim = new UserClaim
                    {
                        ClaimType = claim.Type,
                    };// Show
                    if(userClaims.Any(c=>c.Type == claim.Type +"_Show" && c.Value == "true"))
                    {
                        userClaim.Show = true;
                    }
                    else
                    {
                        userClaim.Show = false;
                    }//Add
                    if (userClaims.Any(c => c.Type == claim.Type + "_Add" && c.Value == "true"))
                    {
                        userClaim.Add = true;
                    }
                    else
                    {
                        userClaim.Add = false;
                    }//Edit
                    if (userClaims.Any(c => c.Type == claim.Type + "_Edit" && c.Value == "true"))
                    {
                        userClaim.Edit = true;
                    }
                    else
                    {
                        userClaim.Edit = false;
                    }//Delete
                    if (userClaims.Any(c => c.Type == claim.Type + "_Delete" && c.Value == "true"))
                    {
                        userClaim.Delete = true;
                    }
                    else
                    {
                        userClaim.Delete = false;
                    }

                    model.Claims.Add(userClaim);
                   
                }
                return model;

            }
            return NotFound("User Not Found");
        }
        [HttpPut("EditUser/{userId}")]
        public async Task<ActionResult<EditUserClaimViewModel>> EditUser(string userId , EditUserClaimViewModel model)
        {
            if(userId != model.Id)
            {
                return BadRequest("User Id Not Match");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user.UserName != model.UserName)
            {
                user.UserName = model.UserName;
              var result =   await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest("UserName is exist Change it ");
                }
            }
            var userclaimsInDb =await _userManager.GetClaimsAsync(user);
            for(int i = 0; i < userclaimsInDb.Count; i++)
            {
                var type = userclaimsInDb[i].Type;
                var length = type.Length;
                var value = userclaimsInDb[i].Value;
                var index = type.IndexOf('_');
                var firstDigit = type.Substring(0, index);
                var test = type;
                var secondDigit = type.Substring(index +1);
                var claimModel = model.Claims.Find(c => c.ClaimType == firstDigit);
                if(claimModel != null)
                {
                    switch (secondDigit)
                    {
                        case "Show":
                            if(claimModel.Show.ToString().ToLower() != value)
                            {
                                await _userManager.ReplaceClaimAsync(user, userclaimsInDb[i], new Claim(type = firstDigit + "_Show", claimModel.Show.ToString().ToLower()));
                               
                            }
                        break;
                        case "Add":
                            if (claimModel.Add.ToString().ToLower() != value)
                            {
                                await _userManager.ReplaceClaimAsync(user, userclaimsInDb[i], new Claim(type = firstDigit + "_Add", claimModel.Add.ToString().ToLower()));

                            }
                            break;
                        case "Edit":
                            if (claimModel.Edit.ToString().ToLower() != value)
                            {
                                await _userManager.ReplaceClaimAsync(user, userclaimsInDb[i], new Claim(type = firstDigit + "_Edit", claimModel.Edit.ToString().ToLower()));

                            }
                            break;
                        case "Delete":
                            if (claimModel.Delete.ToString().ToLower() != value)
                            {
                                await _userManager.ReplaceClaimAsync(user, userclaimsInDb[i], new Claim(type = firstDigit + "_Delete", claimModel.Delete.ToString().ToLower()));

                            }
                            break;

                    }
                }
        
            }
            return Ok();
        }
    }
}
