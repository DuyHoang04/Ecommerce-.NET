using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;

namespace Ecommerce.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<ResponseDto<string>> CreateCategory(CategoryRequest request);
        Task<ResponseDto<List<CategoryWrapper>>> FindAllCategory(int page, int size, string searchName);

        Task<ResponseDto<CategoryWrapper>> FindCategoryById(int categoryId);

        Task<ResponseDto<string>> UpdateCategory(CategoryRequest request, int categoryId);

        Task<ResponseDto<string>> DeleteCategory(int categoryId);



    }
}
