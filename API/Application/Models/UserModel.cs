using System.ComponentModel;
using Application.Models.Article;

namespace Application.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string NickName { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    public IEnumerable<ArticlePreviewModel>? Articles { get; set; }
}