using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPut("update/{userId}")]
        public async Task<ActionResult<ResponseDto<string>>> UpdateUser(UserRequest request,string userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var result = await _userService.UpdateUser(request, userId);
                if(result.Status == 404)
                {
                    return NotFound(result.Message);
                }
                return Ok(result);
            } catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> ChangePassword(ChangePassRequest request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(userId == null)
                {
                    return Unauthorized();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var result = await _userService.ChangePassword(userId, request);
                if (result.Status == 404)
                {
                    return NotFound(result.Message);
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
        public async Task<ActionResult<ResponseDto<string>>> getAllUser()
        {
            try
            {

                var result = await _userService.getAllUser();
                if (result.Status == 404)
                {
                    return NotFound(result.Message);
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

        [HttpGet("find/{userId}")]
        public async Task<ActionResult<ResponseDto<UserWrapper>>> FindUserById(string userId)
        {
            try
            {
                var result = await _userService.findUserById(userId);
                if (result.Status == 404)
                {
                    return NotFound(result.Message);
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

    }
}
