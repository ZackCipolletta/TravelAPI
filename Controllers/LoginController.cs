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
        var token = Generate(user);
        return Ok(token);
      }

      return NotFound("User not found");
    }

    private string Generate(ApplicationUser user)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperWeinerMan5000"));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

      var token = new JwtSecurityToken(_config["https://www.yogihosting.com"],
        _config["https://www.yogihosting.com"],
        claims,
        expires: DateTime.Now.AddMinutes(15),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
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
  }
}