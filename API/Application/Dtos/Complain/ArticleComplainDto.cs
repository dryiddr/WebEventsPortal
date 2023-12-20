using Domain.Complaint;
using Domain.Enums;

namespace Application.Dtos.Complain;

public class ArticleComplainDto
{
    public ArticleComplainCategories Category { get; set; }
    public string? Text { get; set; }
    public Guid ArticleId { get; set; }
}