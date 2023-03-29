
namespace TravelApi.Models
{
    public class UserConstants
    {

        public int UserConstantsId { get; set; }
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { UserName = "jason_admin", EmailAddress = "jason.admin@email.com", Password = "MyPass_w0rd" }
        };
    }
}
