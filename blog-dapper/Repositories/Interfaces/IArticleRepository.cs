using blog_dapper.Models;

namespace Blog_Dapper.Repositories.Interfaces;

public interface IArticleRepository
{
    Article GetArticle(int id);
    List<Article> GetArticles();
    Article CreateArticle(Article article);
    Article UpdateArticle(Article article);
    void DeleteArticle(int id);
    
    List<Article> GetArticlesWithCategory();
}