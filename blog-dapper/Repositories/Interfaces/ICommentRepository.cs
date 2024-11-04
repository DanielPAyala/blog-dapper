using blog_dapper.Models;

namespace Blog_Dapper.Repositories.Interfaces;

public interface ICommentRepository
{
    Comment GetComment(int commentId);
    List<Comment> GetComments();
    Comment CreateComment(Comment comment);
    Comment UpdateComment(Comment comment);
    void DeleteComment(int commentId);
}