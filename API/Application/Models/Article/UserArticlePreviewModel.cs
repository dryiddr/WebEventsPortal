using Domain.Enums;

namespace Application.Models.Article;

public class UserArticlePreviewModel : ArticlePreviewModel
{
    public ArticleStatuses Status { get; set; }
    public int CountViewsPerDay { get; set; }
    public int CountViewsPerWeek { get; set; }
    public int CountViewsPerMonth { get; set; }
}