using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Repository.PaymentRepository;

namespace Ecommerce.Service.PaymentService
{
    public class PaymentService: IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository) {
            _paymentRepository = paymentRepository;
        }

        public Task<ResponseDto<string>> CreatePayment(PaymentRequest request)
        {
           return _paymentRepository.CreatePayment(request);
        }
    }
}
