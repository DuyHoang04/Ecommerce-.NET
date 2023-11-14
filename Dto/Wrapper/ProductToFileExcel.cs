namespace Ecommerce.Dto.Wrapper
{
    public class ProductToFileExcel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = "";


        public decimal Price { get; set; }


        public int Stock { get; set; } = 0;

        public string Images { get; set; }

        public string BrandName { get; set; }

        public string Categories { get; set; }

        public string Status { get; set; } = "Active";

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
