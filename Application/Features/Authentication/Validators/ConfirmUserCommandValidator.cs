using Application.Features.Authentication.Commands;
using FluentValidation;

namespace Application.Features.Authentication.Validators;

public class ConfirmUserCommandValidator: AbstractValidator<ConfirmUserCommand>
{
    public ConfirmUserCommandValidator()
    {
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .Matches(@"^(?=.*\d)")
            .WithMessage("Password must contain at least one number")
            .Matches(@"^(?=.*[@$!%^*?&])")
            .WithMessage("Password must contain at least one special character")
            .Matches(@"^(?=.*[A-Z])")
            .WithMessage("Password must contain at least one uppercase letter");

        
        RuleFor(x => x.PasswordConfirmation)
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");

        RuleFor(x => x.Names)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        
        RuleFor(x => x.Lastnames)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.EmailConfirmationToken)
            .NotNull()
            .NotEmpty();
    }
}