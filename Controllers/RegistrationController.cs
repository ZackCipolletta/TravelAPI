using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RegistrationController : ControllerBase
  {
    private readonly UserManager<UserModel> _userManager;

    public RegistrationController(UserManager<UserModel> userManager)
    {
      _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserModel userModel)
    {
      var userExists = await _userManager.FindByNameAsync(userModel.UserName);
      if (userExists != null)
      {
        return Conflict("User already exists!");
      }

      var result = await _userManager.CreateAsync(userModel, userModel.Password);

      if (result.Succeeded)
      {
        return Ok("User created successfully!");
      }
      else
      {
        var errors = string.Join(",", result.Errors.Select(e => e.Description));
        return StatusCode(StatusCodes.Status500InternalServerError, $"User creation failed! {errors}");
      }
    }
  }
}