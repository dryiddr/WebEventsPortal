using Application.Models.Article;

namespace Application.Models;

public class SearchModel
{
    public IEnumerable<ArticlePreviewModel>? Articles { get; set; }
}