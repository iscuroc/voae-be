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

        }
    }
}