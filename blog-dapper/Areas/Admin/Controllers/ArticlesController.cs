using blog_dapper.Models;
using Blog_Dapper.Repositories;
using Blog_Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Dapper.Areas.Admin.Controllers;

[Area("Admin")]
public class ArticlesController : Controller
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ITagRepository _tagRepository;

    public ArticlesController(IArticleRepository articleRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostingEnvironment, ITagRepository tagRepository)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _hostingEnvironment = hostingEnvironment;
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
        ViewBag.Categories = _categoryRepository.GetListCategories();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Title, Description, State, CategoryId")]Article article)
    {
        if (!ModelState.IsValid) return View(article);
        var mainPath = _hostingEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (files.Count > 0)
        {
            var fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(mainPath, @"images\articles");
            var extension = Path.GetExtension(files[0].FileName);
            if (article.Image != null)
            {
                var imagePath = Path.Combine(mainPath, article.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            using var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create);
            files[0].CopyTo(fileStreams);
            article.Image = @"\images\articles\" + fileName + extension;
        }
        
        _articleRepository.CreateArticle(article);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var article = _articleRepository.GetArticle(id.GetValueOrDefault());
        if (article == null) return NotFound();
        ViewBag.Categories = _categoryRepository.GetListCategories();
        return View(article);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ArticleId, Title, Description, Image, State, CategoryId")] Article article)
    {
        if (id != article.ArticleId) return NotFound();
        if (!ModelState.IsValid) return View(article);
        
        var mainPath = _hostingEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (files.Count > 0)
        {
            var fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(mainPath, @"images\articles");
            var extension = Path.GetExtension(files[0].FileName);
            if (article.Image != null)
            {
                var imagePath = Path.Combine(mainPath, article.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            using var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create);
            files[0].CopyTo(fileStreams);
            article.Image = @"\images\articles\" + fileName + extension;
        }
        
        _articleRepository.UpdateArticle(article);
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult AssignTags(int? id)
    {
        if (id == null) return NotFound();
        var article = _articleRepository.GetArticle(id.GetValueOrDefault());
        if (article == null) return NotFound();
        
        ViewBag.Tags = _tagRepository.GetListTags();
        return View(article);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AssignTags(int articleId, int tagId)
    {
        if (articleId == 0 || tagId == 0)
        {
            return NotFound();
        }
        var articleTags = new ArticleTags
        {
            ArticleId = articleId,
            TagId = tagId
        };
        _tagRepository.AssignTags(articleTags);
        return RedirectToAction(nameof(Index));
    }

    #region

    [HttpGet]
    public IActionResult GetArticles()
    {
        return Json(new { data = _articleRepository.GetArticlesWithCategory() });
    }

    [HttpDelete]
    public IActionResult DeleteArticle(int? id)
    {
        if (id == null) return Json(new { success = false, message = "Error while deleting" });
        _articleRepository.DeleteArticle(id.GetValueOrDefault());
        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}