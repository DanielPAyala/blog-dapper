using blog_dapper.Models;

namespace Blog_Dapper.Repositories;

public interface ICategoryRepository
{
    Category GetCategory(int id);
    List<Category> GetCategories();
    Category CreateCategory(Category category);
    Category UpdateCategory(Category category);
    void DeleteCategory(int id);
}