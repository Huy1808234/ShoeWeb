﻿namespace Project01.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int? Quantity { get; set; }  
        double? Price { get; set; } 
    }
}
