namespace Project01.Models
{
    public class Checkout
    {
        // Billing Address
        public int Id { get; set; } // Khóa chính
        public string?FullName { get; set; }
        public string?Email { get; set; }
        public string?Address { get; set; }
        public string?City { get; set; }
        public string?State { get; set; }
        public string?Zip { get; set; }

        // Payment Information
        public string?NameOnCard { get; set; }
        public string?CreditCardNumber { get; set; }
        public string?ExpMonth { get; set; }
        public string?ExpYear { get; set; }
        public string?CVV { get; set; }
        public List<CartItem> ? CartItems { get; set; } // Danh sách sản phẩm trong giỏ hàng
        public decimal TotalAmount => (decimal)(CartItems?.Sum(item => item.Quantity * item.Product.Price) ?? 0);
    }

}
