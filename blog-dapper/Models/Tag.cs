using System.ComponentModel.DataAnnotations;

namespace blog_dapper.Models;

public class Tag
{
    public Tag()
    {
        Articles = [];    
    }
    
    [Key] public int TagId { get; set; }
    
    [Required(ErrorMessage = "Nombre es requerido")]
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public List<Article> Articles { get; set; }
}