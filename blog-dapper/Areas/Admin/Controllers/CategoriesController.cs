using blog_dapper.Models;
using Blog_Dapper.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Dapper.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _categoryRepository.GetCategories();
        return View(categories);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid) return View(category);
        _categoryRepository.CreateCategory(category);
        return RedirectToAction(nameof(Index));
    }

    #region

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Json(new { data = _categoryRepository.GetCategories() });
    }

    #endregion
}