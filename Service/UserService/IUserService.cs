using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;

namespace Ecommerce.Service.UserService
{
    public interface IUserService
    {
        Task<ResponseDto<string>> UpdateUser(UserRequest request, string userId);

        Task<ResponseDto<string>> ChangePassword(string userId, ChangePassRequest request);

        Task<ResponseDto<List<UserWrapper>>> getAllUser();

        Task<ResponseDto<UserWrapper>> findUserById(string userId);


    }
}
