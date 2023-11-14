using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.OrderItemRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderItemRepository(DataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ResponseDto<string>> AddOrderItem(OrderItemRequest request, string userId, int productId, int? promotionId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var product = await _dbContext.Product.FirstOrDefaultAsync(p => p.ProductId == productId);
                var orderItem = await _dbContext.OrderItems
                    .FirstOrDefaultAsync(
                    od => od.ProductId == productId
                    && od.UserId == userId
                    && od.IsDeleted == false
                    ); 
                var promotion = await _dbContext.Promotions.FirstOrDefaultAsync(pm => pm.PromotionId == promotionId);
                if (product == null)
                {
                    return _response.Response404("Product Not Found");
                }
                if (user == null)
                {
                    return _response.Response404("User Not Found");
                }
                if (orderItem != null)
                {
                    var newQuantity = orderItem.Quantity += request.Quantity;
                    orderItem.TotalPrice = newQuantity * product.Price;
                    orderItem.UpdatedAt = DateTime.Now;
                    _dbContext.OrderItems.Update(orderItem);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    string sqlQuery = "INSERT INTO order_item(quantity, total_price, is_deleted, product_id, createdAt, updatedAt, user_id) " +
                                      "VALUES (@Quantity, @TotalPrice, 0, @ProductId, @CreatedAt, @UpdatedAt, @UserId)";

                    SqlParameter[] parameters = new[]
                     {
                          new SqlParameter("@Quantity", request.Quantity),
                          new SqlParameter("@TotalPrice", request.Quantity * product.Price),
                          new SqlParameter("@CreatedAt", DateTime.Now),
                          new SqlParameter("@UpdatedAt", DateTime.Now),
                          new SqlParameter("@ProductId", product.ProductId),
                          new SqlParameter("@UserId", userId)
                     };

                    _dbContext.Database.ExecuteSqlRaw(sqlQuery, parameters);
                }
                return _response.ResponseSuccess("Add OrderItem Successfully");
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }

        }

        public async Task<ResponseDto<List<OrderItemWrapper>>> GetOrderItemUser(string userId)
        {
            var _response = new ResponseDto<List<OrderItemWrapper>>();  
            try
            {
                var orderItemList = await _dbContext.OrderItems
                                            .Include(ot => ot.Product)
                                            .ThenInclude(p => p.Images)
                                            .Where(ot => ot.UserId == userId && ot.IsDeleted == false)
                                            .ToListAsync();
               

                var result = orderItemList.Select(item =>
                {
                    var imageUrl = item.Product.Images.First()?.OriginalLinkImage;
                    return new OrderItemWrapper
                    {
                        OrderItemId = item.OrderItemId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ProductName = item.Product.Name,
                        Price = item.TotalPrice,
                        ImageUrl = imageUrl!
                    };
                }).ToList();
                return _response.ResponseData(result);
            } catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<string>> UpdateOrderItemForMe(int orderItemId,string userId, bool? increase, bool? decrease)
        {
            var _response = new ResponseDto<string>();
            try
            {
                bool result = false;
                var orderItem = await _dbContext.OrderItems
                    .Include(ot => ot.Product)
                    .FirstOrDefaultAsync(ot => ot.OrderItemId == orderItemId);

                if(orderItem == null)
                {
                    return _response.NotFound("Order Item Not Found");
                }
                if (increase.HasValue)
                {
                    result = IncreaseQuantity(1, orderItem);
                }
                if (decrease.HasValue)
                {
                    result =  DecreaseQuantity(1, orderItem);
                }
                if(!result)
                {
                    return _response.Response404("Something went wrong");

                }
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        private bool IncreaseQuantity(int quantity, OrderItem orderItem)
        {

            try
            {
                var newQuantity = orderItem.Quantity += quantity;
                orderItem.TotalPrice = newQuantity * orderItem.Product.Price;
                orderItem.UpdatedAt = DateTime.Now;
                _dbContext.OrderItems.Update(orderItem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DecreaseQuantity(int quantity, OrderItem orderItem)
        {

            try
            {
                var newQuantity = orderItem.Quantity -= quantity;
                orderItem.TotalPrice = newQuantity * orderItem.Product.Price;
                orderItem.UpdatedAt = DateTime.Now;
                if(newQuantity == 0)
                {
                    _dbContext.OrderItems.Remove(orderItem);
                }
                else
                {
                    _dbContext.OrderItems.Update(orderItem);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ResponseDto<string>> DeleteOrderItemForMe(int orderItemId, string userId)
        {
            var _response = new ResponseDto<string>();
           try
            {
                var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(ot => ot.OrderItemId == orderItemId && ot.UserId == userId);
                if(orderItem == null)
                {
                    return _response.NotFound("Order Item Not Found");
                }
                _dbContext.OrderItems.Remove(orderItem);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Delete Order Item Successfullys");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
