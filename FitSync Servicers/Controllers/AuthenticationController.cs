using FitSync_Servicers.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitSync_Servicers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup([FromBody] Registration registration)
        {
            var userExist = await userManager.FindByNameAsync(registration.UserName);

            if (userExist != null)
                return StatusCode(StatusCodes.Status400BadRequest, new AuthResult { Status = "Error", Message = "User Already Exists!" });

            User user = new User()
            {
                Email = registration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registration.UserName
            };

            var result = await userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResult { Status = "Error", Message = "Internal Server Error" });
            }

            return Ok(new AuthResult { Status = "Success", Message = "User Created Sccuessfully" });
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Signin([FromBody] Login login)
        {
            var userExist = await userManager.FindByEmailAsync(login.Email);

        }
}
