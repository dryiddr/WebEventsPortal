using FluentValidation;
using Application.Dtos.Commentary;

namespace Application.Validation;

public class AddCommentaryDtoValidator : AbstractValidator<AddCommentaryDto>
{
    public AddCommentaryDtoValidator()
    {
        RuleFor(dto => dto.Text).MinimumLength(1);
    }
}