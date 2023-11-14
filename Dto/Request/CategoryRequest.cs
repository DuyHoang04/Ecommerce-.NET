using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto.Request
{
    public class CategoryRequest
    {
        public string Name { get; set; } = "";
        public string? Code { get; set; } = "";

        public bool? IsDeleted { get; set; }
    }
}
