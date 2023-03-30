// using TravelApi.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Security.Claims;

// namespace TravelApi.Controllers
// {
//   [Route("api/[controller]")]
//   [ApiController]
//   public class UserController : ControllerBase
//   {

//     [HttpGet("Admins")]
//     [Authorize(Roles = "Administrator")]
//     public IActionResult AdminsEndpoint()
//     {
//       var currentUser = GetCurrentUser();

//       return Ok($"Hi {currentUser.UserName}!");
//     } 


//     [HttpGet("Public")]
//     public IActionResult Public()
//     {
//       return Ok("Hi, you're on public property");
//     }

//     private ApplicationUser GetCurrentUser()
//     {
//       var identity = HttpContext.User.Identity as ClaimsIdentity;

//       if (identity != null)
//       {
//         var userClaims = identity.Claims;

//         return new ApplicationUser
//         {
//           UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
//           Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
//         };
//       }
//       return null;
//     }
//   }
// }