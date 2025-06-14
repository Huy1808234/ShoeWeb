
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project01.AppData;
using Project01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project01.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDBContext _db;

        // Key lưu chuỗi JSON của giỏ hàng
        public const string CARTKEY = "cart";

        public ProductController(AppDBContext db)
        {
            _db = db;
        }

        // Trang chủ danh sách sản phẩm
        public IActionResult Index()
        {
            return View();
        }

        // Hiển thị chi tiết sản phẩm
        public async Task<IActionResult> Detail(int id)
        {
            // Lấy sản phẩm chi tiết theo ID
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Lấy danh sách 3 sản phẩm ngẫu nhiên khác sản phẩm hiện tại
            var relatedProducts = await _db.Products
                                           .Where(prod => prod.Id != id)
                                           .OrderBy(r => Guid.NewGuid())
                                           .Take(3)
                                           .ToListAsync();

            // Tạo ViewModel để truyền cả sản phẩm chính và sản phẩm liên quan
            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts
            };

            return View(viewModel);
        }

        // Hiển thị danh sách sản phẩm theo danh mục
        public IActionResult Listpro(int id)
        {
            // Lấy danh sách sản phẩm thuộc CategoryId
            var proList = _db.Products.Where(p => p.CategoryId == id).ToList();
            return View(proList);
        }
        // Lấy danh sách giỏ hàng từ Session
        
        private List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsonCart = session.GetString(CARTKEY);
            if (jsonCart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsonCart);
            }
            return new List<CartItem>();
        }

        // Lưu danh sách giỏ hàng vào Session
        private void SaveCartSession(List<CartItem> cart)
        {
            var session = HttpContext.Session;
            string jsonCart = JsonConvert.SerializeObject(cart);
            session.SetString("cart", jsonCart);
        }

        // Thêm sản phẩm vào giỏ hàng
        [Route("addcart/{productid:int}")]
        public IActionResult AddToCart([FromRoute] int productid)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == productid);
            if (product == null)
                return NotFound("Không có sản phẩm.");

            // Lấy giỏ hàng từ Session
            var cart = GetCartItems();

            // Kiểm tra nếu sản phẩm đã có trong giỏ hàng
            var cartItem = cart.FirstOrDefault(c => c.Product.Id == productid);
            if (cartItem != null)
            {
                cartItem.Quantity++; // Nếu đã có, tăng số lượng
            }
            else
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }

            // Lưu giỏ hàng vào Session
            SaveCartSession(cart);

            return RedirectToAction(nameof(Cart));
        }

        [Authorize]
        // Hiển thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            var cart = GetCartItems();
            return View(cart); // Truyền giỏ hàng tới view
        }


        // Xóa một sản phẩm khỏi giỏ hàng
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.Product.Id == productid);

            if (cartItem != null)
            {
                cart.Remove(cartItem); // Xóa sản phẩm khỏi giỏ hàng
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        // Cập nhật số lượng sản phẩm trong giỏ hàng
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.Product.Id == productid);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    cart.Remove(cartItem); // Nếu số lượng <= 0 thì xóa sản phẩm
                }
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [Authorize]
        [Route("/checkout", Name = "checkout")]
        public IActionResult Checkout()
        {
            var cart = GetCartItems(); // Lấy giỏ hàng từ Session

            if (cart == null || !cart.Any())
            {
                return RedirectToAction(nameof(Cart)); // Chuyển hướng nếu giỏ hàng trống
            }

            var checkoutViewModel = new Checkout
            {
                CartItems = cart
            };

            return View("Checkout", checkoutViewModel); // Truyền dữ liệu giỏ hàng sang view Checkout
        }
        [HttpPost("/checkout")]
        public IActionResult Checkout(Checkout Checkout)
        {
            if (ModelState.IsValid)
            {
                // Xử lý lưu đơn hàng vào database
                _db.Checkouts.Add(Checkout);
                _db.SaveChanges();

                return RedirectToAction("Success"); // Chuyển hướng sau khi thành công
            }

            // Truyền lại CartItems nếu có lỗi
            Checkout.CartItems = GetCartItems();
            return View("Checkout", Checkout);
        }
        public IActionResult Success()
        {
            return View();
        }

    }
}
