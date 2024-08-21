using Application.Features.Activities.Commands;
using FluentValidation;

namespace Application.Features.Activities.Validators;

public class ActivityBannerValidator: AbstractValidator<ActivityBannerCommand>
{
    public ActivityBannerValidator()
    {
        RuleFor(x => x.Banner)
            .NotNull()
            .NotEmpty()
            .WithMessage("Banner must not be empty");
    }
}