using Domain.Enums;

namespace Domain.Complaint;

public class ArticleComplaint : BaseComplaint
{
    public ArticleComplainCategories Category { get; set; }
    public Article Article { get; set; }
}