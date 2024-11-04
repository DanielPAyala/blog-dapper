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
}