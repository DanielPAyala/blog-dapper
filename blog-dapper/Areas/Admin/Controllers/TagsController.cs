using blog_dapper.Models;
using Blog_Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Dapper.Areas.Admin.Controllers;

[Area("Admin")]
public class TagsController : Controller
{
    private readonly ITagRepository _tagRepository;
    
    public TagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
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
    public IActionResult Create([Bind("Name")] Tag tag)
    {
        if (!ModelState.IsValid) return View(tag);
        _tagRepository.CreateTag(tag);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var tag = _tagRepository.GetTag(id.GetValueOrDefault());
        if (tag == null) return NotFound();
        return View(tag);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("TagId, Name")] Tag tag)
    {
        if (id != tag.TagId) return NotFound();
        if (!ModelState.IsValid) return View(tag);
        _tagRepository.UpdateTag(tag);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult ArticlesWithTags()
    {
        return View();
    }

    #region

    [HttpGet]
    public IActionResult GetTags()
    {
        return Json(new { data = _tagRepository.GetTags() });
    }
    
    [HttpGet]
    public IActionResult GetArticlesWithTags()
    {
        return Json(new { data = _tagRepository.GetArticlesWithTags() });
    }
    
    [HttpDelete]
    public IActionResult DeleteTag(int? id)
    {
        if (id == null) return Json(new { success = false, message = "Error while deleting" });
        _tagRepository.DeleteTag(id.GetValueOrDefault());
        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}