using Application.Models.Article;

namespace Application.Models;

public class RecommendationModel
{
    public IEnumerable<ArticlePreviewModel> Articles { get; set; }
}