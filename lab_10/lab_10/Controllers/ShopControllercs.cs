using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab_10.Data;

namespace lab_10.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }

        // Akcja Index przyjmuje opcjonalne id kategorii
        public async Task<IActionResult> Index(int? id)
        {
            // 1. Pobieramy wszystkie kategorie do menu bocznego
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = id;

            // 2. Pobieramy artykuły (wszystkie lub przefiltrowane)
            var articlesQuery = _context.Articles.Include(a => a.Category).AsQueryable();

            if (id.HasValue)
            {
                articlesQuery = articlesQuery.Where(a => a.CategoryId == id.Value);
            }

            var articles = await articlesQuery.ToListAsync();
            return View(articles);
        }
    }
}