using lab11.Data;
using lab11.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab11.Pages.Shop
{
    [Authorize] 
    public class OrderModel : PageModel
    {
        private readonly MyDbContext _context;
        public OrderModel(MyDbContext context) => _context = context;

        public class OrderItem
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total => Price * Quantity;
        }

        public List<OrderItem> Summary { get; set; } = new();
        public decimal GrandTotal { get; set; }

        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string PaymentMethod { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin")) return RedirectToPage("/Index");

            await LoadSummary();

            if (!Summary.Any()) return RedirectToPage("/Shop/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSummary();
                return Page();
            }

            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie.StartsWith("article"))
                {
                    Response.Cookies.Delete(cookie);
                }
            }

            TempData["SuccessMessage"] = $"Dziêkujemy {FullName}! Zamówienie op³acone przez {PaymentMethod} zosta³o przyjête.";
            return RedirectToPage("Success", new
            {
                name = FullName,
                address = Address,
                method = PaymentMethod
            });
        }

        private async Task LoadSummary()
        {
            Summary = new List<OrderItem>();
            GrandTotal = 0;

            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("article"))
                {
                    if (int.TryParse(cookie.Key.Replace("article", ""), out int id))
                    {
                        var art = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                        if (art != null)
                        {
                            int qty = int.Parse(cookie.Value);
                            Summary.Add(new OrderItem { Name = art.Name, Price = art.Price, Quantity = qty });
                            GrandTotal += art.Price * qty;
                        }
                    }
                }
            }
        }
    }
}