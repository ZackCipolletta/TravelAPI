using Microsoft.AspNetCore.Identity;

namespace TravelApi.Models
{
    public class UserModel
    {
          public int UserModelId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

    }
}