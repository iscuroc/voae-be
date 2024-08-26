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
            .WithMessage("Banner is required")
            .Must(x => x.Length > 0)
            .WithMessage("Banner is required")
            .Must(x => x.Length < 3 * 1024 * 1024)
            .WithMessage("Banner size must not exceed 3MB")
            .Must(x => x.ContentType is "image/jpeg" or "image/jpg" or "image/png")
            .WithMessage("Banner must be an image file (jpg, jpeg, png)");
    }
}