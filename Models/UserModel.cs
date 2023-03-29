using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApi.Models
{
  public class UserModel
  {
    [ForeignKey("UserConstants")]
    public int UserConstantId { get; set; }
    public int UserModelId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string EmailAddress { get; set; }
    public string ConfirmPassword { get; set; }

  }
}