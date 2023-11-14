using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Repository.CategoryRepository;

namespace Ecommerce.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<string>> CreateCategory(CategoryRequest request)
        {
            return await _categoryRepository.CreateCategory(request);
        }

        public async Task<ResponseDto<string>> DeleteCategory(int categoryId)
        {
            return await _categoryRepository.DeleteCategory(categoryId);
        }

        public async Task<ResponseDto<List<CategoryWrapper>>> FindAllCategory(int page, int size, string searchName)
        {
            return await _categoryRepository.FindAllCategory(page, size, searchName);
        }

        public async Task<ResponseDto<CategoryWrapper>> FindCategoryById(int categoryId)
        {
            return await _categoryRepository.FindCategoryById(categoryId);
        }

        public async Task<ResponseDto<string>> UpdateCategory(CategoryRequest request, int categoryId)
        {
           return await _categoryRepository.UpdateCategory(request, categoryId);
        }
    }
}
