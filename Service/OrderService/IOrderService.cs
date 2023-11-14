using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;

namespace Ecommerce.Service.OrderService
{
    public interface IOrderService
    {
        Task<ResponseDto<string>> CreateOrder(OrderRequest request, string userId);

        Task<ResponseDto<List<OrderWrapper>>> GetOrderForMe(string userId);

    }
}
