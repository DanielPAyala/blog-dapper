using System.Data;
using System.Data.SqlClient;
using blog_dapper.Models;
using Blog_Dapper.Repositories.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Dapper.Repositories;

public class CategoryRepository(IConfiguration configuration) : ICategoryRepository
{
    private readonly IDbConnection _dbConnection = new SqlConnection(configuration.GetConnectionString("SQLLocalDB"));

    public Category GetCategory(int id)
    {
        const string sql = "SELECT * FROM Category WHERE CategoryId = @Id";
        return _dbConnection.Query<Category>(sql, new { Id = id }).SingleOrDefault();
    }

    public List<Category> GetCategories()
    {
        const string sql = "SELECT * FROM Category";
        return _dbConnection.Query<Category>(sql).ToList();
    }

    public Category CreateCategory(Category category)
    {
        const string sql =
            "INSERT INTO Category (Name, CreatedAt) VALUES (@Name, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _dbConnection.Query<int>(sql, new
        {
            category.Name,
            CreatedAt = DateTime.Now
        }).Single();
        category.CategoryId = id;
        return category;
    }

    public Category UpdateCategory(Category category)
    {
        const string sql = "UPDATE Category SET Name = @Name WHERE CategoryId = @CategoryId";
        _dbConnection.Execute(sql, category);
        return category;
    }

    public void DeleteCategory(int id)
    {
        const string sql = "DELETE FROM Category WHERE CategoryId = @Id";
        _dbConnection.Execute(sql, new { Id = id });
    }

    public IEnumerable<SelectListItem> GetListCategories()
    {
        const string sql = "SELECT * FROM Category";
        var categories = _dbConnection.Query<Category>(sql).ToList();
        return new SelectList(categories, "CategoryId", "Name");
    }
}