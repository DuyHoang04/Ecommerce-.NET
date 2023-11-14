using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model
{
    [Table("promotion")]
    public class Promotion
    {
        [Key]
        [Column("promotion_id")]
        public int PromotionId { get; set; }

        [Column("code")]
        [Required]
        public string Code { get; set; } = "";

        [Column("discount_percent")]
        [Required]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public decimal DiscountPercent { get; set; } = 0;

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
