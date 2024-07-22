using Application.Features.Authentication.Commands;
using FluentValidation;

namespace Application.Features.Authentication.Validators
{
    public class PasswordResetCommandValidator : AbstractValidator<PasswordResetCommand>
    {
        public PasswordResetCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");

            RuleFor(x => x.ResetToken)
                .NotEmpty()
                .WithMessage("Reset token is required.");
        }
    }
}