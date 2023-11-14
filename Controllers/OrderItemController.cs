using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Service.OrderItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/order-item")]
    public class OrderItemController: ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(IOrderItemService orderItemService, ILogger<OrderItemController> logger) {
            _orderItemService = orderItemService;
            _logger = logger;
        }

        [HttpPost("add/{productId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> AddOrderItem(OrderItemRequest request, int productId, [FromQuery] int? promotionId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(userId == null)
                {
                    return Unauthorized();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var result = await _orderItemService.AddOrderItem(request, userId, productId, promotionId);
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

        [HttpGet("get-for-me/")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<List<OrderItemWrapper>>>> GetOrderItemUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _orderItemService.GetOrderItemUser(userId);
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

        [HttpPatch("update/{orderItemId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<List<OrderItemWrapper>>>> GetOrderItemUser(
            int orderItemId,
            [FromQuery] bool? increase,
            [FromQuery] bool? decrease
        )
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _orderItemService.UpdateOrderItemForMe(orderItemId, userId, increase, decrease);
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

        [HttpDelete("delete/{orderItemId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> DeleteOrderItem(int orderItemId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _orderItemService.DeleteOrderItemForMe(orderItemId, userId);
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
