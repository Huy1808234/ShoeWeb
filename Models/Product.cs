using System.ComponentModel.DataAnnotations;

namespace Project01.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // ID sản phẩm, không thể null vì là khóa chính

        public string? Name { get; set; } // Tên sản phẩm, có thể null

        public string? Description { get; set; } // Mô tả sản phẩm, có thể null

        public double? Price { get; set; } // Giá sản phẩm, có thể null

        public string? ImagePath { get; set; } // Đường dẫn ảnh, có thể null

        public int? CategoryId { get; set; } // ID danh mục, có thể null vì có liên kết với bảng Category

        public Category? Category { get; set; } // Đối tượng Category để thiết lập quan hệ nhiều - một

        public ICollection<OrderProduct>? OrderProducts { get; set; } // Tập hợp các OrderProduct cho mối quan hệ nhiều - nhiều

        // Thuộc tính mới
        public string? Availability { get; set; } // Tình trạng sản phẩm (ví dụ: "Còn hàng", "Hết hàng")

        public string? Model { get; set; } // Model của sản phẩm

        public string? Manufacturer { get; set; } // Nhà sản xuất sản phẩm

        public int? Quantity { get; set; } // Số lượng sản phẩm còn trong kho
    }
}
