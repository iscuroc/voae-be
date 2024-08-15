using Application.Features.Activities.Commands;
using FluentValidation;

namespace Application.Features.Activities.Validators;

public class ApproveActivityValidator : AbstractValidator<ApproveActivityCommand>
{
    public ApproveActivityValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Activity ID must be a number");

        RuleFor(x => x.ReviewerObservation)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(800)
            .WithMessage("The reviewers observation must not be empty and not exceed 800 characters.");
    }
}
