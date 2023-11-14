using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Repository.OrderItemRepository;

namespace Ecommerce.Service.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository) {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<ResponseDto<string>> AddOrderItem(OrderItemRequest request, string userId, int productId, int? promotionId)
        {
            return await _orderItemRepository.AddOrderItem(request, userId, productId, promotionId);  
        }

        public async Task<ResponseDto<string>> DeleteOrderItemForMe(int orderItemId, string userId)
        {
            return await _orderItemRepository.DeleteOrderItemForMe(orderItemId, userId);
        }

        public async Task<ResponseDto<List<OrderItemWrapper>>> GetOrderItemUser(string userId)
        {
            return await _orderItemRepository.GetOrderItemUser(userId);
        }

        public async Task<ResponseDto<string>> UpdateOrderItemForMe(int orderItemId, string userId, bool? increase, bool? decrease)
        {
            return await _orderItemRepository.UpdateOrderItemForMe(orderItemId,userId, increase, decrease);
        }
    }
}
