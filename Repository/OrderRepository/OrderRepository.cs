using Ecommerce.Data;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.OrderRepository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(DataContext dbContext, UserManager<ApplicationUser> userManager, ILogger<OrderRepository> logger) {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResponseDto<string>> CreateOrder(OrderRequest request, string userId)
        {
            var _response = new ResponseDto<string>();  
            try
            {
                var payment = await _dbContext.Payments.FirstOrDefaultAsync(pm => pm.PaymentId == request.PaymentId);
                var user = await _userManager.FindByIdAsync(userId);
                
                if(payment == null)
                {
                    return _response.Response404("Payment Not Found");
                }
                if (user == null)
                {
                    return _response.Response404("User Not Found");
                }

                var newOrder = new Order
                {
                    Code = request.Code,
                    TotalAmount = payment.Amount,
                    CustomerName = user.UserName!,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber!,
                    Note = request.Note,
                    Status = request.Status,
                    User = user,
                    Payment = payment,
                };

                if (request.OrderItemIds != null && request.OrderItemIds.Any())
                {
                    foreach (var orderItemId in request.OrderItemIds)
                    {
                        var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(ot => ot.OrderItemId == orderItemId);

                        if (orderItem != null)
                        {
                            newOrder.OrderItemList.Add(orderItem);
                            newOrder.TotalAmount += orderItem.TotalPrice;
                            orderItem.IsDeleted = true;
                        }
                        _dbContext.OrderItems.Update(orderItem!);
                    }

                }
                _dbContext.Orders.Add(newOrder);
                await _dbContext.SaveChangesAsync();
                return _response.ResponseSuccess("Order Successfully");

            }
            catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<List<OrderWrapper>>> GetOrderForMe(string userId)
        {
            var _response = new ResponseDto<List<OrderWrapper>>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    return _response.NotFound("User Not Found");
                }
                var orderList = await _dbContext.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.User)
                    .Include(o => o.Payment)
                    .Include(o => o.OrderItemList)
                    .ThenInclude(ot => ot.Product)
                    .ThenInclude(p => p.Images)
                    .ToListAsync();


                var result = orderList.Select(item => new OrderWrapper
                {
                    OrderId = item.OrderId,
                    Code = item.Code,
                    CustomerName = item.CustomerName,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    TotalAmount = item.TotalAmount,
                    Note = item.Note,
                    Status = item.Status,
                    UserId = user.Id,
                    PaymentMethod = item.Payment.PaymentMethod,
                    AmountPayment = item.Payment.Amount,
                    OrderItems = item.OrderItemList.Select(ot =>
                    {
                        var imageUrl = ot.Product.Images.First()?.OriginalLinkImage;
                        return new OrderItemWrapper
                        {
                            OrderItemId = ot.OrderItemId,
                            ProductId = ot.ProductId,
                            Quantity = ot.Quantity,
                            ProductName = ot.Product.Name,
                            Price = ot.TotalPrice,
                            ImageUrl = imageUrl!
                        };
                    }).ToList(),
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                }).ToList();



                return _response.ResponseData(result);
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
