using System.Data;
using System.Data.SqlClient;
using blog_dapper.Models;
using Blog_Dapper.Repositories.Interfaces;
using Dapper;

namespace Blog_Dapper.Repositories;

public class CommentRepository(IConfiguration configuration) : ICommentRepository
{
    private readonly IDbConnection _dbConnection = new SqlConnection(configuration.GetConnectionString("SQLLocalDB"));
    
    public Comment GetComment(int commentId)
    {
        const string sql = "SELECT * FROM Comment WHERE CommentId = @Id";
        return _dbConnection.Query<Comment>(sql, new { Id = commentId }).SingleOrDefault();
    }

    public List<Comment> GetComments()
    {
        const string sql = "SELECT * FROM Comment";
        return _dbConnection.Query<Comment>(sql).ToList();
    }

    public Comment CreateComment(Comment comment)
    {
        const string sql =
            "INSERT INTO Comment (Title, Message, ArticleId, CreatedAt) VALUES (@Title, @Message, @ArticleId, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _dbConnection.Query<int>(sql, new
        {
            comment.Title,
            comment.Message,
            comment.ArticleId,
            CreatedAt = DateTime.Now
        }).Single();
        comment.CommentId = id;
        return comment;
    }

    public Comment UpdateComment(Comment comment)
    {
        const string sql = "UPDATE Comment SET Title = @Title, Message = @Message WHERE CommentId = @CommentId";
        _dbConnection.Execute(sql, comment);
        return comment;
    }

    public void DeleteComment(int commentId)
    {
        const string sql = "DELETE FROM Comment WHERE CommentId = @Id";
        _dbConnection.Execute(sql, new { Id = commentId });
    }

    public List<Comment> GetCommentsWithArticle()
    {
        const string sql =
            "SELECT c.CommentId, c.Title, c.Message, c.ArticleId, c.CreatedAt, a.ArticleId, a.Title FROM Comment c INNER JOIN Article a ON c.ArticleId = a.ArticleId ORDER BY c.CreatedAt DESC";
        var comments = _dbConnection.Query<Comment, Article, Comment>(sql, (c, a) =>
        {
            c.Article = a;
            return c;
        }, splitOn: "ArticleId").ToList();
        return comments.Distinct().ToList();
    }
}