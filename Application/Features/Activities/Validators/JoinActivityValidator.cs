using Application.Features.Activities.Commands;
using FluentValidation;

namespace Application.Features.Activities.Validators;

public class JoinActivityValidator : AbstractValidator<JoinActivityCommand>
{
    public JoinActivityValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Scopes)
            .NotNull()
            .NotEmpty();

        RuleForEach(x => x.Scopes)
            .NotNull()
            .IsInEnum();
    }
}