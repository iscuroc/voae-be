using Application.Features.Authentication.Commands;
using FluentValidation;

namespace Application.Features.Authentication.Validators;

public class ConfirmUserCommandValidator: AbstractValidator<ConfirmUserCommand>
{
    public ConfirmUserCommandValidator()
    {
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Password must be at least 8 characters long " +
                         "and include at least one uppercase letter, " +
                         "one lowercase letter, one number, and one special character.");
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