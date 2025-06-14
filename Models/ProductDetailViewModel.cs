namespace Project01.Models
{
    public class ProductDetailViewModel
    {
        public required Product Product { get; set; }

        // Danh sách 3 sản phẩm ngẫu nhiên khác sản phẩm hiện tại
        public required List<Product> RelatedProducts { get; set; }

    }
}
