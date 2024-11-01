using Application.Features.Authentication.Commands;
using FluentValidation;

namespace Application.Features.Authentication.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"^(?=.*\d)")
                .WithMessage("Password must contain at least one number")
                .Matches(@"^(?=.*[@$!%^*?&])")
                .WithMessage("Password must contain at least one special character")
                .Matches(@"^(?=.*[A-Z])")
                .WithMessage("Password must contain at least one uppercase letter");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");

            RuleFor(x => x.ResetToken)
                .NotEmpty()
                .WithMessage("Reset token is required.");
        }
    }
}