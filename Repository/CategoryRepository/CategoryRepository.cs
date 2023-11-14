using AutoMapper;
using Azure;
using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto<string>> CreateCategory(CategoryRequest request)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var CategoryExists = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == request.Name && c.IsDeleted == false);
                if (CategoryExists != null)
                {
                    return _response.Response404("Category Already Exists");
                }
                var category = new Category
                {
                    Name = request.Name,
                    Code = request.Code!,
                };
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Category created successfully");

            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> DeleteCategory(int categoryId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var category = await _dbContext.Categories.FirstOrDefaultAsync(b => b.CategoryId == categoryId);
                if (category == null)
                {
                    return _response.NotFound("Category Not Found");
                }
                category.IsDeleted = true;
                category.UpdatedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Delete Category Successfully");
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<List<CategoryWrapper>>> FindAllCategory(int page, int size, string searchName)
        {
            var _response = new ResponseDto<List<CategoryWrapper>>();
            var query = _dbContext.Categories.AsQueryable();

            try
            {
                query = query.Where(c => c.IsDeleted == false);
                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    query = query.Where(b => b.Name.Contains(searchName));
                }

                var totalItems = await query.CountAsync();
                var categories = await query.Skip((page - 1) * size).Take(size).ToListAsync();
                var result = _mapper.Map<List<CategoryWrapper>>(categories);
                return _response.ResponseData(result);
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<CategoryWrapper>> FindCategoryById(int categoryId)
        {
            var _response = new ResponseDto<CategoryWrapper>();
            try
            {
                var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);
                if (category == null)
                {
                    return _response.NotFound("Category Not Found");
                }
                var result = _mapper.Map<CategoryWrapper>(category);
                return _response.ResponseData(result);
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> UpdateCategory(CategoryRequest request, int categoryId)
        {
            var _response = new ResponseDto<string>();

           try
            {
                var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);
                if (category == null)
                {
                    return _response.NotFound("Category Not Found");
                }
                category.Name = request.Name;
                category.UpdatedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Update Category Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
