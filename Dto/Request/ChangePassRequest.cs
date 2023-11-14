namespace Ecommerce.Dto.Request
{
    public class ChangePassRequest
    {
        public string CurrentPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
