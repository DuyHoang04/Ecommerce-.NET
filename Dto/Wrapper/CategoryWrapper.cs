namespace Ecommerce.Dto.Wrapper
{
    public class CategoryWrapper
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
