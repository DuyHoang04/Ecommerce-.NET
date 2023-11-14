using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;

namespace Ecommerce.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<ResponseDto<string>> UpdateUser(UserRequest request, string userId);

        Task<ResponseDto<List<UserWrapper>>> GetAllUser();

        Task<ResponseDto<string>> ChangePassword(string userId, ChangePassRequest request);

        Task<ResponseDto<UserWrapper>> findUserById(string userId); 



    }
}
