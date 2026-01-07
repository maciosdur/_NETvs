using lab09.Interfaces;
using lab09.Models;

namespace lab09.Services
{
    public class DictionaryArticlesContext : IArticlesContext
    {

        private static Dictionary<int, Article> _articles = new Dictionary<int, Article>();
        private static int _nextId = 1; 

        public void AddArticle(Article article)
        {
            article.Id = _nextId++; 
            _articles.Add(article.Id, article);
        }

        public void DeleteArticle(int id)
        {
            _articles.Remove(id);
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _articles.Values.OrderBy(a => a.Id).ToList();
        }

        public Article? GetArticleById(int id)
        {
            _articles.TryGetValue(id, out var article);
            return article;
        }

        public void UpdateArticle(Article updatedArticle)
        {
            if (_articles.ContainsKey(updatedArticle.Id))
            {

                _articles[updatedArticle.Id] = updatedArticle;
            }

        }
    }
}
