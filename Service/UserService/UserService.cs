using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Repository.UserRepository;

namespace Ecommerce.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<string>> ChangePassword(string userId, ChangePassRequest request)
        {
            return await _userRepository.ChangePassword(userId, request);
        }

        public async Task<ResponseDto<UserWrapper>> findUserById(string userId)
        {
            return await _userRepository.findUserById(userId);
        }

        public async Task<ResponseDto<List<UserWrapper>>> getAllUser()
        {
            return await _userRepository.GetAllUser();
        }

        public async Task<ResponseDto<string>> UpdateUser(UserRequest request, string userId)
        {
            return await _userRepository.UpdateUser(request, userId);
        }
    }
}
