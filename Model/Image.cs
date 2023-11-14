using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    [Table("images")]
    public class Image
    {
        [Key]
        [Column("image_id")]
        public int ImageId { get; set; }

        [Column("origin_link_image")]
        public string OriginalLinkImage { get; set; } = "";

        [Column("local_link_image")]
        public string LocalLinkImage { get; set; } = "";

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();
    }
}
