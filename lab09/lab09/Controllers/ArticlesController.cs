using lab09.Interfaces; // Używamy naszego interfejsu
using lab09.Models;      // Używamy klasy Article
using Microsoft.AspNetCore.Mvc;

namespace lab09.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesContext _context;

        // Wstrzykiwanie zależności (DI) - Kontener serwisów automatycznie 
        // dostarczy zaimplementowany serwis (ListArticlesContext lub DictionaryArticlesContext)
        public ArticlesController(IArticlesContext context)
        {
            _context = context;
        }

        // ---------------------------------------------
        // A. READ: Index (Lista wszystkich towarów)
        // GET: /Articles
        // ---------------------------------------------
        public IActionResult Index()
        {
            var articles = _context.GetAllArticles();
            return View(articles); // Przekazanie listy Article do widoku
        }

        // ---------------------------------------------
        // B. READ: Details (Szczegóły pojedynczego towaru)
        // GET: /Articles/Details/5
        // ---------------------------------------------
        public IActionResult Details(int id)
        {
            var article = _context.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // ---------------------------------------------
        // C. CREATE: Create (Dodawanie nowego towaru)
        // ---------------------------------------------

        // GET: /Articles/Create
        public IActionResult Create()
        {
            return View(); // Wyświetlenie pustego formularza
        }

        // POST: /Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Ochrona przed atakami CSRF
        public IActionResult Create(Article article)
        {
            // Sprawdzenie, czy model jest poprawny (np. czy wymagane pola są wypełnione)
            if (ModelState.IsValid)
            {
                _context.AddArticle(article);
                return RedirectToAction(nameof(Index)); // Przekierowanie do listy po dodaniu
            }
            return View(article); // Powrót do formularza z błędami
        }

        // ---------------------------------------------
        // D. UPDATE: Edit (Modyfikacja towaru)
        // ---------------------------------------------

        // GET: /Articles/Edit/5
        public IActionResult Edit(int id)
        {
            var article = _context.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article); // Wyświetlenie formularza wypełnionego danymi
        }

        // POST: /Articles/Edit/5
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

        // ---------------------------------------------
        // E. DELETE: Delete (Usuwanie towaru)
        // ---------------------------------------------

        // GET: /Articles/Delete/5
        public IActionResult Delete(int id)
        {
            var article = _context.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article); // Wyświetlenie widoku potwierdzającego usunięcie
        }

        // POST: /Articles/Delete/5 (Potwierdzenie usunięcia)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.DeleteArticle(id);
            return RedirectToAction(nameof(Index));
        }
    }
}