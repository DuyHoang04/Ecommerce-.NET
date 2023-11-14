namespace Ecommerce.Dto.Request
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "";
        public string TransactionDate { get; set; } = "";
    }
}
