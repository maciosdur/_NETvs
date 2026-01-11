using lab11.Data;
using lab11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab11.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;

        public IndexModel(MyDbContext context)
        {
            _context = context;
        }

        // W³aœciwoœci zamiast ViewBag
        public IList<Article> Articles { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;
        public int? SelectedCategoryId { get; set; }

        // Parametr 'id' zostanie automatycznie wy³apany z adresu URL (np. /Shop?id=5)
        public async Task OnGetAsync(int? id)
        {
            SelectedCategoryId = id;

            // 1. Pobieramy kategorie do menu
            Categories = await _context.Categories.ToListAsync();

            // 2. Budujemy zapytanie o artyku³y
            var articlesQuery = _context.Articles.Include(a => a.Category).AsQueryable();

            if (id.HasValue)
            {
                articlesQuery = articlesQuery.Where(a => a.CategoryId == id.Value);
            }

            Articles = await articlesQuery.ToListAsync();
        }

        public async Task<IActionResult> OnGetAddToCart(int id)
        {
            string cookieKey = "article" + id;
            string cookieValue = Request.Cookies[cookieKey];

            int count = 1;
            if (cookieValue != null && int.TryParse(cookieValue, out int existingCount))
            {
                count = existingCount + 1;
            }

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Pamiêtaj przez tydzieñ
                HttpOnly = true,
                IsEssential = true
            };

            Response.Cookies.Append(cookieKey, count.ToString(), options);

            // Wracamy na tê sam¹ stronê (zachowuj¹c ewentualny filtr kategorii)
            return RedirectToPage("./Index", new { id = SelectedCategoryId });
        }
    }
}