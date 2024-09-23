using Application.Features.Activities.Commands;
using FluentValidation;

namespace Application.Features.Activities.Validators;

public class PublishActivityValidator: AbstractValidator<PublishActivityCommand>
{
    public PublishActivityValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id is required");
    }
}