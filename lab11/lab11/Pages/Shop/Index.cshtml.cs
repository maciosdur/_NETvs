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

        public IList<Article> Articles { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;
        public int? SelectedCategoryId { get; set; }

        public async Task OnGetAsync(int? id)
        {
            SelectedCategoryId = id;
            Categories = await _context.Categories.ToListAsync();

            var query = _context.Articles.Include(a => a.Category).AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(a => a.CategoryId == id);
            }

            Articles = await query.Take(3).ToListAsync();
        }

        public async Task<IActionResult> OnGetAddToCart(int id)
        {
            if (User.IsInRole("Admin")) return RedirectToPage("/Index");
            string cookieKey = "article" + id;
            string cookieValue = Request.Cookies[cookieKey];

            int count = 1;
            if (cookieValue != null && int.TryParse(cookieValue, out int existingCount))
            {
                count = existingCount + 1;
            }

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true
            };

            Response.Cookies.Append(cookieKey, count.ToString(), options);

            return RedirectToPage("./Index", new { id = SelectedCategoryId });
        }
    }
}