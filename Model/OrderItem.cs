using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model
{
    [Table("order_item")]
    public class OrderItem
    {
        [Key]
        [Column("order_item_id")]
        public int OrderItemId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();

        /*
        [Column("promotion_id")]
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; } = new Promotion();
        */

        [Column("order_id")]
        public int? OrderId { get; set; }
        public Order Order { get; set; } = new Order();

        [Column("user_id")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = new ApplicationUser();

        [Column("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
