using FitSync_Servicers.Authentication;
using FitSync_Servicers.Data;
using FitSync_Servicers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitSync_Servicers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _context = context;
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
            } else
            {
                UserProfile userProfile = new UserProfile()
                {
                    UserId = user.Id,
                    Name = registration.Name,
                    Email = registration.Email,
                    DateOfBirth = registration.DateOfBirth,
                    Telephone = registration.Telephone,
                    Weight = registration.Weight,
                    Height = registration.Height,
                    BloodType = registration.BloodType,
                    Gender = registration.Gender,
                    DailyCalorieGoal = registration.DailyCalorieGoal,
                    DailyExerciseGoal = registration.DailyExerciseGoal,
                };

                _context.userProfile.Add(userProfile);
                var resultUserProfile = await _context.SaveChangesAsync();
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if(await roleManager.RoleExistsAsync(UserRoles.User)) 
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new AuthResult { Status = "Success", Message = "User Created Sccuessfully" });
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Signin([FromBody] Login login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user != null & await userManager.CheckPasswordAsync(user, login.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.UserName
                });
            }

            return Unauthorized();
        }


    }
}
