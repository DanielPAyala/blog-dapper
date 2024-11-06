using blog_dapper.Models;
using Blog_Dapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Dapper.Areas.Admin.Controllers;

[Area("Admin")]
public class CommentsController : Controller
{
    private readonly ICommentRepository _commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /*[HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CommentId, Name")] Comment comment)
    {
        if (!ModelState.IsValid) return View(comment);
        _commentRepository.CreateComment(comment);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var comment = _commentRepository.GetComment(id.GetValueOrDefault());
        if (comment == null) return NotFound();
        return View(comment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("CommentId, Name")] Comment comment)
    {
        if (id != comment.CommentId) return NotFound();
        if (!ModelState.IsValid) return View(comment);
        _commentRepository.UpdateComment(comment);
        return RedirectToAction(nameof(Index));
    }*/

    #region

    [HttpGet]
    public IActionResult GetComments()
    {
        return Json(new { data = _commentRepository.GetCommentsWithArticle() });
    }
    
    [HttpDelete]
    public IActionResult DeleteComment(int? id)
    {
        if (id == null) return Json(new { success = false, message = "Error while deleting" });
        _commentRepository.DeleteComment(id.GetValueOrDefault());
        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}