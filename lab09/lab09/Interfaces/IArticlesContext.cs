using lab09.Models;

namespace lab09.Interfaces
{
    public interface IArticlesContext
    { 
        IEnumerable<Article> GetAllArticles();

        Article? GetArticleById(int id);

        void AddArticle(Article article);

        void UpdateArticle(Article article);

        void DeleteArticle(int id);
    }
}
