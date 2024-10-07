using System.ComponentModel.DataAnnotations;

namespace blog_dapper.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "El nombre de categoría es obligatorio")]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}