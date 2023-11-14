using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;

namespace Ecommerce.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<ResponseDto<string>> CreatePayment(PaymentRequest request);
    }
}
