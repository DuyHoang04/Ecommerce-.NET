namespace Ecommerce.Dto.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = String.Empty;

        public string RefreshToken { get; set; } = String.Empty;


        public int Status { get; set; } = 200;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = String.Empty;

    }
}
