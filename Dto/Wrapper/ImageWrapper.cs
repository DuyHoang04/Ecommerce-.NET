namespace Ecommerce.Dto.Wrapper
{
    public class ImageWrapper
    {
        public int ImageId { get; set; }
        public string OriginalLinkImage { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public int ProductId { get; set; }
    }
}
