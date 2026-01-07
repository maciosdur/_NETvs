using lab09.Interfaces; 
using lab09.Models;      
using Microsoft.AspNetCore.Mvc;

namespace lab09.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesContext _context;

        
        public ArticlesController(IArticlesContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var articles = _context.GetAllArticles();
            return View(articles); 
        }

        public IActionResult Details(int id)
        {
            var article = _context.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }


        public IActionResult Create()
        {
            return View(); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                _context.AddArticle(article);
                return RedirectToAction(nameof(Index)); 
            }
            return View(article); 
        }

        public IActionResult Edit(int id)
        {
            var article = _context.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.UpdateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }


        public IActionResult Delete(int id)
        {
            var article = _context.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.DeleteArticle(id);
            return RedirectToAction(nameof(Index));
        }
    }
}