using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;

namespace Ecommerce.Repository.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task<ResponseDto<string>> CreatePayment(PaymentRequest request);

    }
}
