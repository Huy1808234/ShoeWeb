using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project01.AppData;
using Project01.Models;

namespace Project01.Components
{
    public class CategoryViewComponent: ViewComponent
    {
        private readonly AppDBContext _db;

        public CategoryViewComponent(AppDBContext db) => this._db = db;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> catelist = await this._db.Categories.ToListAsync();
            return View(catelist);
        }
    }
}
