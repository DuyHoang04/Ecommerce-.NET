using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Helper;
using Ecommerce.Model;
using Ecommerce.Service.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly Cloudinary _cloudinary;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, Cloudinary cloudinary, IProductService productService)
        {
            _logger = logger;
            _cloudinary = cloudinary;
            _productService = productService;
        }



        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> CreateProduct(ProductRequest request)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }
                var result = await _productService.CreateProduct(request);
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

        [HttpPut("update/{productId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> UpdateProduct(ProductRequest request, int productId)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }
                var result = await _productService.UpdateProduct(request, productId);
                if (result.Status == 404)
                {
                    return BadRequest(result.Message);
                }
                if (result.Status == 500)
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

        [HttpDelete("delete/{productId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> DeleteProduct(int productId)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }
                var result = await _productService.DeleteProduct(productId);
                if (result.Status == 404)
                {
                    return BadRequest(result.Message);
                }
                if (result.Status == 500)
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

        [HttpGet("getAll")]

        public async Task<ActionResult<ResponseDto<List<ProductWrapper>>>> FindAllProduct(
                  [FromQuery] int page = 1,
                  [FromQuery] int size = 10,
                  [FromQuery] string searchName = "",
                  [FromQuery] int? brandId = null,
                  [FromQuery] int? categoryId = null
        )
        {
            try
            {
                var result = await _productService.FindAllProduct(page, size, searchName, categoryId, brandId);
                if (result.Status == 500)
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

        [HttpGet("find/{productId}")]

        public async Task<ActionResult<ResponseDto<ProductDetailWrapper>>> FindProductById(int productId)
        {
            try
            {
                var result = await _productService.FindProductById(productId);
                if(result.Status == 404)
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

        [HttpGet("download")]
        public async Task<IActionResult> Download(CancellationToken ct)
        {
            try
            {
                var file = await _productService.ExportFileExcel(ct);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "products.xlsx");
            } catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
          
        }
    }
}
