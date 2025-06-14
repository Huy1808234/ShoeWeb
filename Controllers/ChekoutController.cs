using Microsoft.AspNetCore.Mvc;
using Project01.Models;

namespace Project01.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View( "Checkout");
        }

        [HttpPost]
        public IActionResult Index(Checkout model)
        {
            if (ModelState.IsValid)
            {
                // Xử lý thanh toán tại đây
                ViewBag.Message = "Thanh toán thành công!";
                return View("Success", model);
            }
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
