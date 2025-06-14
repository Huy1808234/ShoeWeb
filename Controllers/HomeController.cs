using Microsoft.AspNetCore.Mvc;
using Project01.AppData;
using Project01.Models;
using System.Diagnostics;

namespace Project01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;
        public HomeController(ILogger<HomeController> logger, AppDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Product>  prolist = _db.Products.Take(6).ToList();
            return View(prolist);
        }
            
        public IActionResult Privacy()
        {
            List<Category> castlist = _db.Categories.Take(2).ToList();
            return View(castlist);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
