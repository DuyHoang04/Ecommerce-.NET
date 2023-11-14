using Ecommerce.Model;

namespace Ecommerce.Dto.Request
{
    public class UserRequest
    {
        public string Address { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Role { get; set; } = Roles.User;


    }
}
