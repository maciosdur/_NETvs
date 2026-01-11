using lab11.Data;
using lab11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab11.Pages.Shop
{
    public class CartModel : PageModel
    {
        private readonly MyDbContext _context;
        public CartModel(MyDbContext context) { _context = context; }

        public class CartItem
        {
            public Article Article { get; set; }
            public int Quantity { get; set; }
        }

        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalSum { get; set; }

        public async Task OnGetAsync()
        {
            TotalSum = 0;
            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("article"))
                {
                    int articleId = int.Parse(cookie.Key.Replace("article", ""));
                    int quantity = int.Parse(cookie.Value);

                    var article = await _context.Articles.FindAsync(articleId);
                    if (article != null)
                    {
                        CartItems.Add(new CartItem { Article = article, Quantity = quantity });
                        TotalSum += article.Price * quantity;
                    }
                }
            }
            CartItems = CartItems.OrderBy(x => x.Article.Name).ToList();
        }

        public IActionResult OnGetIncrease(int id) => ModifyQuantity(id, 1);
        public IActionResult OnGetDecrease(int id) => ModifyQuantity(id, -1);
        public IActionResult OnGetRemove(int id)
        {
            Response.Cookies.Delete("article" + id);
            return RedirectToPage();
        }

        private IActionResult ModifyQuantity(int id, int change)
        {
            string key = "article" + id;
            int current = int.Parse(Request.Cookies[key] ?? "0");
            int newVal = current + change;

            if (newVal <= 0) Response.Cookies.Delete(key);
            else Response.Cookies.Append(key, newVal.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(7) });

            return RedirectToPage();
        }
    }
}
