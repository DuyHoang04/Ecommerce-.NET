using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }


        [Column("name")]
        public string Name { get; set; } = "";

        [Column("price")]
        public decimal Price { get; set; } 

        [Column("stock")]
        public int Stock { get; set; } = 0;


        public virtual ICollection<Image> Images { get; set; } = new List<Image>();

        [Column("brand_id")]
        public int BrandId { get; set; }

        public Brand Brand { get; set; } = new Brand();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        [Column("status")]
        public string Status { get; set; } = "Active";

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
