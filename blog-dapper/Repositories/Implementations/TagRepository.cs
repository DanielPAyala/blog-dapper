using System.Data;
using System.Data.SqlClient;
using blog_dapper.Models;
using Blog_Dapper.Repositories.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Dapper.Repositories;

public class TagRepository(IConfiguration configuration) : ITagRepository
{
    private readonly IDbConnection _dbConnection = new SqlConnection(configuration.GetConnectionString("SQLLocalDB"));
    
    public Tag GetTag(int id)
    {
        const string sql = "SELECT * FROM Tag WHERE TagId = @Id";
        return _dbConnection.Query<Tag>(sql, new { Id = id }).SingleOrDefault();
    }

    public List<Tag> GetTags()
    {
        const string sql = "SELECT * FROM Tag";
        return _dbConnection.Query<Tag>(sql).ToList();
    }

    public Tag CreateTag(Tag tag)
    {
        const string sql =
            "INSERT INTO Tag (Name, CreatedAt) VALUES (@Name, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _dbConnection.Query<int>(sql, new
        {
            tag.Name,
            CreatedAt = DateTime.Now
        }).Single();
        tag.TagId = id;
        return tag;
    }

    public Tag UpdateTag(Tag tag)
    {
        const string sql = "UPDATE Tag SET Name = @Name WHERE TagId = @TagId";
        _dbConnection.Execute(sql, tag);
        return tag;
    }

    public void DeleteTag(int id)
    {
        const string sql = "DELETE FROM Tag WHERE TagId = @Id";
        _dbConnection.Execute(sql, new { Id = id });
    }

    public IEnumerable<SelectListItem> GetListTags()
    {
        const string sql = "SELECT * FROM Tag";
        var tags = _dbConnection.Query<Tag>(sql).ToList();
        return new SelectList(tags, "TagId", "Name");
    }

    public List<Article> GetArticlesWithTags()
    {
        const string sql = "SELECT a.ArticleId, a.Title, t.TagId, t.Name FROM Article a \nINNER JOIN ArticleTags ae ON ae.ArticleId = a.ArticleId\nINNER JOIN Tag t ON t.TagId = ae.TagId";
        return _dbConnection.Query<Article, Tag, Article>(sql, (article, tag) =>
        {
            article.Tags.Add(tag);
            return article;
        }, splitOn: "TagId").ToList();
    }
}