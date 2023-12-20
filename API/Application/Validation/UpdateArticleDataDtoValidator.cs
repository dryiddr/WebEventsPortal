using FluentValidation;
using Application.Dtos.Article;
using Domain.Enums;

namespace Application.Validation;

public class UpdateArticleDataDtoValidator : AbstractValidator<UpdateArticleDataDto>
{
    public UpdateArticleDataDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Must(name => !string.IsNullOrEmpty(name)).When(dto => dto.Status == ArticleStatuses.Published);
        RuleFor(dto => dto.Text)
            .Must(name => !string.IsNullOrEmpty(name)).When(dto => dto.Status == ArticleStatuses.Published);
    }
}