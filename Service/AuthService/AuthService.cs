using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Repository.AuthRepository;

namespace Ecommerce.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<ResponseDto<string>> Register(RegisterRequest request,string role)
        {
            return await _authRepository.Register(request, role);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _authRepository.Login(request);
        }

        public async Task<string> Logout(LoginRequest request)
        {
            return await _authRepository.Login(request);
        }
    }
}
