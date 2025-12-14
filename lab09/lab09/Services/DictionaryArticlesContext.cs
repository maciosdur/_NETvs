using lab09.Interfaces;
using lab09.Models;

namespace lab09.Services
{
    public class DictionaryArticlesContext : IArticlesContext
    {
        // Statyczny słownik przechowujący towary, gdzie kluczem jest Id
        private static Dictionary<int, Article> _articles = new Dictionary<int, Article>();
        private static int _nextId = 1; // Licznik do automatycznego nadawania Id

        public void AddArticle(Article article)
        {
            article.Id = _nextId++; // Nadaj unikalny Id
            _articles.Add(article.Id, article);
        }

        public void DeleteArticle(int id)
        {
            // Usuwanie w słowniku jest bardzo szybkie
            _articles.Remove(id);
        }

        public IEnumerable<Article> GetAllArticles()
        {
            // Zwracamy listę wartości posortowaną po Id
            return _articles.Values.OrderBy(a => a.Id).ToList();
        }

        public Article? GetArticleById(int id)
        {
            // Pobieranie po kluczu w słowniku jest bardzo szybkie
            _articles.TryGetValue(id, out var article);
            return article;
        }

        public void UpdateArticle(Article updatedArticle)
        {
            if (_articles.ContainsKey(updatedArticle.Id))
            {
                // W słowniku możemy po prostu zastąpić obiekt
                _articles[updatedArticle.Id] = updatedArticle;
            }
            // Uwaga: W praktyce lepiej zaktualizować tylko pola, aby nie stracić
            // ewentualnych referencji, ale dla prostoty in-memory, zastąpienie działa.
        }
    }
}
