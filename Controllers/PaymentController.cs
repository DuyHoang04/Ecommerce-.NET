using CloudinaryDotNet.Actions;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Ecommerce.Service.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController: ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(
            IPaymentService paymentService,
            ILogger<PaymentService> logger,
            UserManager<ApplicationUser> userManager
        )
        {
            _paymentService = paymentService;
            _logger = logger;
            _userManager = userManager;
        }  

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<string>>> CreatePayment(PaymentRequest request)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role != "Admin")
                {
                    return Unauthorized();
                }
                var result = await _paymentService.CreatePayment(request);
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
