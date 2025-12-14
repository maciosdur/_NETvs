using lab09.Interfaces;
using lab09.Models;

namespace lab09.Services
{
    public class ListArticlesContext : IArticlesContext
    {
        // Statyczna lista przechowująca towary (in-memory storage)
        private static List<Article> _articles = new List<Article>();
        private static int _nextId = 1; // Licznik do automatycznego nadawania Id

        public void AddArticle(Article article)
        {
            article.Id = _nextId++; // Nadaj unikalny Id
            _articles.Add(article);
        }

        public void DeleteArticle(int id)
        {
            var articleToRemove = _articles.FirstOrDefault(a => a.Id == id);
            if (articleToRemove != null)
            {
                _articles.Remove(articleToRemove);
            }
        }

        public IEnumerable<Article> GetAllArticles()
        {
            // Zwracamy kopię, aby uniknąć niekontrolowanej modyfikacji
            return _articles.OrderBy(a => a.Id).ToList();
        }

        public Article? GetArticleById(int id)
        {
            return _articles.FirstOrDefault(a => a.Id == id);
        }

        public void UpdateArticle(Article updatedArticle)
        {
            var existingArticle = _articles.FirstOrDefault(a => a.Id == updatedArticle.Id);

            if (existingArticle != null)
            {
                // Aktualizujemy tylko pola, które mogą się zmienić
                existingArticle.Name = updatedArticle.Name;
                existingArticle.Price = updatedArticle.Price;
                existingArticle.ExpirationDate = updatedArticle.ExpirationDate;
                existingArticle.Category = updatedArticle.Category;
            }
        }
    }
}
