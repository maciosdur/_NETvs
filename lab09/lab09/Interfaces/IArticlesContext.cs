using lab09.Models;

namespace lab09.Interfaces
{
    public interface IArticlesContext
    {
        // READ - Pobierz wszystkie towary
        IEnumerable<Article> GetAllArticles();

        // READ - Pobierz towar po Id
        Article? GetArticleById(int id);

        // CREATE - Dodaj nowy towar
        void AddArticle(Article article);

        // UPDATE - Zaktualizuj istniejący towar
        void UpdateArticle(Article article);

        // DELETE - Usuń towar po Id
        void DeleteArticle(int id);
    }
}
