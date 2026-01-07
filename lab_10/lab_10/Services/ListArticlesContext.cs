using lab_10.Interfaces;
using lab_10.Models;

namespace lab_10.Services
{
    public class ListArticlesContext : IArticlesContext
    {
        private static List<Article> _articles = new List<Article>();
        private static int _nextId = 1;

        public void AddArticle(Article article)
        {
            article.Id = _nextId++;
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
                existingArticle.Name = updatedArticle.Name;
                existingArticle.Price = updatedArticle.Price;
                existingArticle.ExpirationDate = updatedArticle.ExpirationDate;
                existingArticle.Category = updatedArticle.Category;
            }
        }
    }
}
