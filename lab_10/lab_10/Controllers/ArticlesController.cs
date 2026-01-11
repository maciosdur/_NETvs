using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_10.Data;
using lab_10.Models;

namespace lab_10.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticlesController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // --- INDEX & DETAILS ---
        public async Task<IActionResult> Index()
        {
            var articles = await _context.Articles.Include(a => a.Category).ToListAsync();
            return View(articles);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null) return NotFound();
            return View(article);
        }

        // --- CREATE ---
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            if (ModelState.IsValid)
            {
                if (article.FormFile != null)
                {
                    article.ImagePath = await SaveImage(article.FormFile);
                }

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // --- EDIT (Zadanie 2: Brak zmiany obrazka) ---
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles.FindAsync(id);
            if (article == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article)
        {
            if (id != article.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var oldArticle = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                    article.ImagePath = oldArticle.ImagePath;

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // --- DELETE ---
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null) return NotFound();
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImagePath))
                {
                    DeleteImage(article.ImagePath);
                }
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // --- METODY POMOCNICZE ---
        private bool ArticleExists(int id) => _context.Articles.Any(e => e.Id == id);

        private async Task<string> SaveImage(IFormFile file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(wwwRootPath, "upload", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

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