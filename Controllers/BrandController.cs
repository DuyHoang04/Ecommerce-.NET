using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Ecommerce.Service.BrandService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }


        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> CreateBrand(BrandRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != Roles.Admin)
                {
                    return Unauthorized();
                }
                var result = await _brandService.CreateBrand(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<BrandWrapper>>> GetAllBrand()
        {
            try
            {
                var result = await _brandService.GetAllBrand();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("find/{brandId}")]
        public async Task<ActionResult<BrandWrapper>> FindBrandById(int brandId)
        {
            try
            {
                var result = await _brandService.FindBrandById(brandId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{brandId}")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateBrand(BrandRequest request, int brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != Roles.Admin)
                {
                    return Unauthorized();
                }
                var result = await _brandService.UpdateBrand(request, brandId);
                if (result.Status == 404)
                {
                    return NotFound(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{brandId}")]
        public async Task<ActionResult<string>> DeleteBrand(int brandId)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != Roles.Admin)
                {
                    return Unauthorized();
                }
                var result = await _brandService.DeleteBrand(brandId);
                if (result.Status == 404)
                {
                    return NotFound(result.Message);
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
