using blog_dapper.Models;
using Blog_Dapper.Repositories;
using Blog_Dapper.Repositories.Interfaces;
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
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid) return View(category);
        _categoryRepository.CreateCategory(category);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var category = _categoryRepository.GetCategory(id.GetValueOrDefault());
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("CategoryId, Name")] Category category)
    {
        if (id != category.CategoryId) return NotFound();
        if (!ModelState.IsValid) return View(category);
        _categoryRepository.UpdateCategory(category);
        return RedirectToAction(nameof(Index));
    }

    #region

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Json(new { data = _categoryRepository.GetCategories() });
    }
    
    [HttpDelete]
    public IActionResult DeleteCategory(int? id)
    {
        if (id == null) return Json(new { success = false, message = "Error while deleting" });
        _categoryRepository.DeleteCategory(id.GetValueOrDefault());
        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}