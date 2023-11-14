using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto.Wrapper
{
    public class OrderWrapper
    {
        public int OrderId { get; set; }
        public string Code { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string Note { get; set; } = "";
        public string Status { get; set; } = "";
        public string UserId { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
        public decimal AmountPayment { get; set; }

        public List<OrderItemWrapper>? OrderItems { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
