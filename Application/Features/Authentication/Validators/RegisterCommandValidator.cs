using Application.Features.Authentication.Commands;
using FluentValidation;

namespace Application.Features.Authentication.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches(@"^[a-zA-Z0-9._%+-]+@unah\.(edu\.)?hn$")
            .WithMessage("Should be a valid UNAH email address");
    }
}