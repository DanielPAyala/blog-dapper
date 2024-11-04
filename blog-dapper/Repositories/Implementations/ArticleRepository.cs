using System.Data;
using System.Data.SqlClient;
using blog_dapper.Models;
using Blog_Dapper.Repositories.Interfaces;
using Dapper;

namespace Blog_Dapper.Repositories;

public class ArticleRepository(IConfiguration configuration) : IArticleRepository
{
    private readonly IDbConnection _dbConnection = new SqlConnection(configuration.GetConnectionString("SQLLocalDB"));

    public Article GetArticle(int id)
    {
        const string sql = "SELECT * FROM Article WHERE ArticleId = @Id";
        return _dbConnection.Query<Article>(sql, new { Id = id }).SingleOrDefault();
    }

    public List<Article> GetArticles()
    {
        const string sql = "SELECT * FROM Article";
        return _dbConnection.Query<Article>(sql).ToList();
    }

    public Article CreateArticle(Article article)
    {
        const string sql =
            "INSERT INTO Article (Title, Description, Image, State, CreatedAt, CategoryId) VALUES (@Title, @Description, @Image, @State, @CreatedAt, @CategoryId); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _dbConnection.Query<int>(sql, new
        {
            article.Title,
            article.Description,
            article.Image,
            article.State,
            CreatedAt = DateTime.Now,
            article.CategoryId
        }).Single();
        article.ArticleId = id;
        return article;
    }

    public Article UpdateArticle(Article article)
    {
        const string sql =
            "UPDATE Article SET Title = @Title, Description = @Description, Image = @Image, State = @State, CategoryId = @CategoryId WHERE ArticleId = @ArticleId";
        _dbConnection.Execute(sql, article);
        return article;
    }

    public void DeleteArticle(int id)
    {
        const string sql = "DELETE FROM Article WHERE ArticleId = @Id";
        _dbConnection.Execute(sql, new { Id = id });
    }

    public List<Article> GetArticlesWithCategory()
    {
        const string sql =
            "SELECT a.ArticleId, a.Title, a.Description, a.Image, a.State, a.CreatedAt, a.CategoryId, c.CategoryId, c.Name, c.CreatedAt FROM Article a INNER JOIN Category c ON a.CategoryId = c.CategoryId ORDER BY a.CreatedAt DESC";
        var articles = _dbConnection.Query<Article, Category, Article>(sql, (a, c) =>
        {
            a.Category = c;
            return a;
        }, splitOn: "CategoryId");

        return articles.Distinct().ToList();
    }
}