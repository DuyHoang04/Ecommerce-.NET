using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;

namespace Ecommerce.Repository.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        Task<ResponseDto<string>> AddOrderItem(OrderItemRequest request, string userId, int productId, int? promotionId);
        Task<ResponseDto<List<OrderItemWrapper>>> GetOrderItemUser(string userId);

        Task<ResponseDto<string>> UpdateOrderItemForMe(int orderItemId, string userId, bool? increase, bool? decrease);

        Task<ResponseDto<string>> DeleteOrderItemForMe(int orderItemId, string userId);



    }
}
