using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Ecommerce.Service.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(
            IOrderService orderService,
            ILogger<OrderController> logger,
            UserManager<ApplicationUser> userManager
        )
        {
            _orderService = orderService;
            _logger = logger;
            _userManager = userManager;
        }


        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> CreateOrder(OrderRequest request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                
                var result = await _orderService.CreateOrder(request, userId);
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

        [HttpGet("get-for-me")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<OrderWrapper>>> GetOrderForMe()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _orderService.GetOrderForMe(userId);
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
    }
}
