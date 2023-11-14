using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Ecommerce.Repository.OrderRepository;

namespace Ecommerce.Service.OrderService
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseDto<string>> CreateOrder(OrderRequest request, string userId)
        {
            return await _orderRepository.CreateOrder(request, userId);
        }

        public async Task<ResponseDto<List<OrderWrapper>>> GetOrderForMe(string userId)
        {
            return await _orderRepository.GetOrderForMe(userId);
        }
    }
}
