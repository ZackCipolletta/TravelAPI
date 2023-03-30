using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private IConfiguration _config;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginController(IConfiguration config, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
      _config = config;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
      var user = await Authenticate(userLogin);

      if (user != null)
      {
        return Ok("Successfully logged in.");
      }

      return NotFound("User not found");
    }

    private async Task<ApplicationUser> Authenticate(UserLogin userLogin)
    {
      var user = await _userManager.FindByNameAsync(userLogin.UserName);
      if (user == null)
      {
        return null;
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

      if (result.Succeeded)
      {
        return user;
      }

      return null;
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return Ok("Successfully logged out.");
    }
  }
}