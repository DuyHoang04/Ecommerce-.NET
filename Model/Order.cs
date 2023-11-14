using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model
{
    [Table("orders")]
    public class Order
    {

        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("code")]
        public string Code { get; set; } = "";

        [Column("customer_name")]
        [Required]
        public string CustomerName { get; set; } = "";

        [Column("address")]
        [Required]

        public string Address { get; set; } = "";

        [Column("phone_number")]
        [Required]
        public string PhoneNumber { get; set; } = "";

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        [Column("note")]
        public string Note { get; set; } = "";

        [Column("status")]
        public string Status { get; set; } = "";

        [Column("user_id")]
        public string UserId { get; set; } = "";

        public ApplicationUser User { get; set; } = new ApplicationUser();

        [Column("payment_id")]
        public int PaymentId { get; set; }

        public Payment Payment { get; set; } = new Payment();

        public virtual ICollection<OrderItem> OrderItemList { get; set; } = new List<OrderItem>();

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
