using Domain;
using Domain.Enums;

namespace Application.Dtos.Article;

public class CreateArticleDto
{
    public ArticleStatuses Status { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string? Text { get; set; }
    public ICollection<TagDto>? Tags { get; set; }
}