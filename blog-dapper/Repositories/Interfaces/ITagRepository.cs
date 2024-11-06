using blog_dapper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Dapper.Repositories.Interfaces;

public interface ITagRepository
{
    Tag GetTag(int id);
    List<Tag> GetTags();
    Tag CreateTag(Tag tag);
    Tag UpdateTag(Tag tag);
    void DeleteTag(int id);
    IEnumerable<SelectListItem> GetListTags();
    
    List<Article> GetArticlesWithTags();
    ArticleTags AssignTags(ArticleTags articleTags);
}