﻿namespace Ecommerce.Dto.Wrapper
{
    public class OrderItemWrapper
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }    
    }
}
