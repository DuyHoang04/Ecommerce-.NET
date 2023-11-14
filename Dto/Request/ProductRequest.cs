using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Dto.Request
{
    public class ProductRequest
    {
        public string Name { get; set; } = "";

        public decimal Price { get; set; }

        public int Stock { get; set; } = 0;

        public int BrandId { get; set; }
        public List<int> CategoryIds { get; set; }

        public string Status { get; set; } = "Active";

        public bool? IsDeleted { get; set; } = false;

        public List<IFormFile>? Images { get; set; }

    }
}
