using System.ComponentModel.DataAnnotations;

namespace Project01.Models
{
    public class CartItem
    {
        [Key]  // Đánh dấu CartItemId là khóa chính
        public int CartItemId { get; set; }
        // Số lượng sản phẩm trong giỏ hàng
        public int Quantity { get; set; }

        // Thông tin sản phẩm trong giỏ hàng
        public required Product Product { get; set; }
    }
}
