using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; } = "";

        [Column("code")]
        [Required]
        public string Code { get; set; } = "";

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Product> ProductList { get; set; } = new List<Product>();

    }
}
