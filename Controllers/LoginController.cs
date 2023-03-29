using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    private readonly TravelApiContext _context;

    public LoginController(TravelApiContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }


    [AllowAnonymous]
[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] UserModel user)
{
  if (ModelState.IsValid)
  {
    // check if user already exists
    var existingUser = await _context.UserModel.FirstOrDefaultAsync(u => u.UserName == user.UserName || u.EmailAddress == user.EmailAddress);
    if (existingUser != null)
    {
      return BadRequest("User already exists");
    }

    // create new user
    var newUser = new UserModel
    {
      UserName = user.UserName,
      EmailAddress = user.EmailAddress,
      Password = user.Password // for simplicity, we are not encrypting the password in this example
    };

    _context.UserModel.Add(newUser);
    await _context.SaveChangesAsync();

    // add new user to UserConstants
    var userConstants = await _context.UserConstant.FirstOrDefaultAsync();
    if (userConstants == null)
    {
      userConstants = new UserConstant();
      _context.UserConstant.Add(userConstants);
      await _context.SaveChangesAsync();
    }
    userConstants.Users.Add(newUser);
    await _context.SaveChangesAsync();

    return Ok();
  }

  return BadRequest(ModelState);
}


    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
      var user = Authenticate(userLogin);

      if (user != null)
      {
        var token = Generate(user);
        return Ok(token);
      }

      return NotFound("User not found");
    }

    private string Generate(UserModel user)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperWeinerMan5000"));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
            };

      var token = new JwtSecurityToken(_config["https://www.yogihosting.com"],
        _config["https://www.yogihosting.com"],
        claims,
        expires: DateTime.Now.AddMinutes(15),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel Authenticate(UserLogin userLogin)
    {
      var userConstants = _context.UserConstant.FirstOrDefault(); // assuming there is only one UserConstants object in the database
      if (userConstants != null)
      {
        var currentUser = userConstants.Users.FirstOrDefault(o => o.UserName.ToLower() == userLogin.UserName.ToLower() && o.Password == userLogin.Password);

        if (currentUser != null)
        {
          return currentUser;
        }
      }

      return null;
    }
  }
}







// public IActionResult Index(string message)
// {
//     ViewBag.Message = message;
//     return View();
// }

// [HttpPost]
// public IActionResult Index(string username, string password)
// {
//     if ((username != "secret") || (password != "secret"))
//         return View((object)"Login Failed");

//     var accessToken = GenerateJSONWebToken();
//     SetJWTCookie(accessToken);

//     return RedirectToAction("FlightReservation");
// }



// private string GenerateJSONWebToken()
// {
//     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MynameisJamesBond007"));
//     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//     var token = new JwtSecurityToken(
//         issuer: "https://www.yogihosting.com",
//         audience: "https://www.yogihosting.com",
//         expires: DateTime.Now.AddHours(3),
//         signingCredentials: credentials
//         );

//     return new JwtSecurityTokenHandler().WriteToken(token);
// }

// private void SetJWTCookie(string token)
// {
//     var cookieOptions = new CookieOptions
//     {
//         HttpOnly = true,
//         Expires = DateTime.UtcNow.AddHours(3),
//     };
//     Response.Cookies.Append("jwtCookie", token, cookieOptions);
// }


// public async Task<IActionResult> FlightReservation()
// {
//     var jwt = Request.Cookies["jwtCookie"];

//     List<Reservation> reservationList = new List<Reservation>();

//     using (var httpClient = new HttpClient())
//     {
//         httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
//         using (var response = await httpClient.GetAsync("https://localhost:44360/Reservation")) // change API URL to yours 
//         {
//             if (response.StatusCode == System.Net.HttpStatusCode.OK)
//             {
//                 string apiResponse = await response.Content.ReadAsStringAsync();
//                 reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
//             }

//             if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//             {
//                 return RedirectToAction("Index", new { message = "Please Login again" });
//             }
//         }
//     }

//     return View(reservationList);
// }



// using (var httpClient = new HttpClient())
// {
//     httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
//     using (var response = await httpClient.GetAsync("https://localhost:44360/Reservation")) // change API URL to yours 
//     {
//         if (response.StatusCode == System.Net.HttpStatusCode.OK)
//         {
//             string apiResponse = await response.Content.ReadAsStringAsync();
//             reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
//         }

//         if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//         {
//             return RedirectToAction("Index", new { message = "Please Login again" });
//         }
//     }
// }