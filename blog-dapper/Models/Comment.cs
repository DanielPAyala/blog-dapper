using System.ComponentModel.DataAnnotations;

namespace blog_dapper.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    
    [Required(ErrorMessage = "El título es obligatorio")]
    public string Title { get; set; }
    
    [Required]
    [StringLength(300, MinimumLength = 10, ErrorMessage = "El mensaje debe tener entre 10 y 300 caracteres")]
    public string Message { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int ArticleId { get; set; }
    
    public virtual Article Article { get; set; }
}