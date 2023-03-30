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

      var result = await _userManager.CreateAsync(userModel, userModel.PasswordHash);

      if (userExists != null)
      {
        return Conflict("User already exists!");
      }

      else if (result.Succeeded)
      {
        var user = await _userManager.FindByNameAsync(userModel.UserName);
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

        return Created("", new { message = "User created successfully" });
      }
      else
      {
        return Created("", new { message = "There was an error creating user" });
      }
    }
  }
}