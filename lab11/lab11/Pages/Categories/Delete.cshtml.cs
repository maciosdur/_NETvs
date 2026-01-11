using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Data;
using lab11.Models;

namespace lab11.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DeleteModel(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();
            
            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            // 1. Znajdź kategorię wraz z przypisanymi do niej artykułami
            var category = await _context.Categories
                .Include(c => c.Articles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category != null)
            {
                // 2. Przejdź pętlą po wszystkich artykułach w tej kategorii i usuń ich zdjęcia
                foreach (var article in category.Articles)
                {
                    if (!string.IsNullOrEmpty(article.ImagePath))
                    {
                        DeleteImage(article.ImagePath);
                    }
                }

                // 3. Usuń kategorię
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        // Metoda pomocnicza do usuwania plików
        private void DeleteImage(string fileName)
        {
            var path = Path.Combine(_hostEnvironment.WebRootPath, "upload", fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}