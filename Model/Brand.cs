using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    [Table("brands")]
    public class Brand
    {
        [Key]
        [Column("brand_id")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = "";

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Product> ProductList { get; set; } = new List<Product>();
    }
}
