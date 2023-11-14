namespace Ecommerce.Dto.Wrapper
{
    public class BrandWrapper
    {
        public int BrandId { get; set; }

        public string Name { get; set; } = "";
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
