using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Service.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> CreateCategory(CategoryRequest request)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var result = await _categoryService.CreateCategory(request);
                if (result.Status == 404)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ResponseDto<List<CategoryWrapper>>>> FindAllCategories(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string searchName = ""
        )
        {
            try
            {
                var result = await _categoryService.FindAllCategory(page, size, searchName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("find/{categoryId}")]
        public async Task<ActionResult<ResponseDto<CategoryWrapper>>> FindCategoryById(int categoryId)
        {
            try
            {
                var result = await _categoryService.FindCategoryById(categoryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{categoryId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> UpdateCategory(CategoryRequest request, int categoryId)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var result = await _categoryService.UpdateCategory(request, categoryId);
                if (result.Status == 404)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{categoryId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> DeleteCategory(int categoryId)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }

                var result = await _categoryService.DeleteCategory(categoryId);
                if (result.Status == 404)
                {
                    return BadRequest(result.Message);
                }
                if(result.Status == 500)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
