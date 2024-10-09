using System.ComponentModel.DataAnnotations;

namespace blog_dapper.Models;

public class Article
{
    [Key] public int ArticleId { get; set; }

    [Required(ErrorMessage = "Título es requerido")]
    public string Title { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "El contenido debe tener entre 10 y 1000 caracteres")]
    public string Description { get; set; }

    public string Image { get; set; }
    public bool State { get; set; }
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Categoría es requerida")]
    public int CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
}