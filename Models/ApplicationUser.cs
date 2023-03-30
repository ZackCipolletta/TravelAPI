using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace TravelApi.Models
{
    public class ApplicationUser : IdentityUser
    {
      [JsonIgnore]
      public override string PhoneNumber { get; set; }

      [JsonIgnore]
      public override bool TwoFactorEnabled  { get; set; }
      
    }
}
