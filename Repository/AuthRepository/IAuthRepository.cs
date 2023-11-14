using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;

namespace Ecommerce.Repository.AuthRepository
{
    public interface IAuthRepository
    {
        Task<ResponseDto<string>> Register(RegisterRequest request, string role);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
