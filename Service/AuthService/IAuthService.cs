using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;

namespace Ecommerce.Service.AuthService
{
    public interface IAuthService
    {
        Task<ResponseDto<string>> Register(RegisterRequest request, string role);
        Task<LoginResponse> Login(LoginRequest request);

    }
}
