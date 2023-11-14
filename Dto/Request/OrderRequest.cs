namespace Ecommerce.Dto.Request
{
    public class OrderRequest
    {
        public string Code { get; set; } = "";
        public string Note { get; set; } = "";
        public string Status { get; set; } = "";
        public int PaymentId { get; set; }

        public List<int> OrderItemIds { get; set; } = new List<int>();
    }
}
