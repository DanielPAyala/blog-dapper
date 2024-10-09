using System.ComponentModel.DataAnnotations;

namespace blog_dapper.Models;

public class Category
{
    [Key] public int CategoryId { get; set; }

    [Required(ErrorMessage = "El nombre de categoría es requerido")]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Article> Articles { get; set; }
}