using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model
{
    [Table("ratings")]
    public class Rating
    {
        [Key]
        [Column("rating_id")]
        public int RatingId { get; set; }

        [Column("comment")]
        [Required]
        public string Comment { get; set; }

        [Column("start_point")]
        [Required]
        public double StartPoint { get; set; }

        [Column("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("user_id")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = new ApplicationUser();

        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();

    }
}
