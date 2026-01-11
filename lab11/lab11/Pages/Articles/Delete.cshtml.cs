using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Data;
using lab11.Models;

namespace lab11.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly lab11.Data.MyDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public DeleteModel(lab11.Data.MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Dodajemy .Include(a => a.Category), żeby na podsumowaniu było widać nazwę kategorii
            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null) return NotFound();

            Article = article;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles.FindAsync(id);

            if (article != null)
            {
                Article = article;

                // LOGIKA USUWANIA ZDJĘCIA 
                if (!string.IsNullOrEmpty(Article.ImagePath))
                {
                    DeleteImage(Article.ImagePath);
                }

                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        // Metoda pomocnicza przeniesiona 1:1 z Twojego kontrolera MVC
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
