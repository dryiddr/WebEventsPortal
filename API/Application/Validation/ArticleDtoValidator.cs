using FluentValidation;
using Application.Dtos.Article;
using Domain.Enums;

namespace Application.Validation;

public class ArticleDtoValidator : AbstractValidator<CreateArticleDto>
{
    public ArticleDtoValidator()
    {
        RuleFor(dto => dto.Status).Must(statuses =>
            statuses is ArticleStatuses.Published or ArticleStatuses.InDraft);
        RuleFor(dto => dto.Text).NotNull().NotEmpty().When(dto => dto.Status == ArticleStatuses.Published);
    }    
}