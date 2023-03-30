using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TravelApi.Models;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public RegistrationController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ApplicationUser userModel)
        {
            var userExists = await _userManager.FindByNameAsync(userModel.UserName);
            if (userExists != null)
            {
                return Conflict("User already exists!");
            }

            var result = await _userManager.CreateAsync(userModel, userModel.PasswordHash);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userModel.UserName);
                var token = Generate(user);

                return Ok(new { token });
            }
            else
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                return StatusCode(StatusCodes.Status500InternalServerError, $"User creation failed! {errors}");
            }
        }

        private string Generate(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(_config["JwtIssuer"],
                _config["JwtAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}