using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
       [HttpGet]
      // [Authorize(Policy ="Booking_Show")]
       [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Booking_Show")]
       // [Authorize(AuthenticationSchemes = "Bearer")]
        public IEnumerable<string> Get() => new string[] { "ahmed", "Esmail" };
    }
}
