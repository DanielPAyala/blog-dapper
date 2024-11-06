using System.ComponentModel.DataAnnotations;

namespace blog_dapper.Models;

public class ArticleTags
{
    [Key]
    public int ArticleId { get; set; }
    [Key]
    public int TagId { get; set; }
}