using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.PaymentRepository
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly DataContext _dbContext;

        public PaymentRepository(DataContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<string>> CreatePayment(PaymentRequest request)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var paymentExists = await _dbContext.Payments.FirstOrDefaultAsync(pm => pm.PaymentMethod == request.PaymentMethod);
                if (paymentExists != null)
                {
                    return _response.Response404("Payment Already Exist");
                }
                var newPayment = new Payment
                {
                    Amount = request.Amount,
                    PaymentMethod = request.PaymentMethod,
                    TransactionDate = request.TransactionDate,
                };
                _dbContext.Payments.Add(newPayment);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Cerate Payment Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
