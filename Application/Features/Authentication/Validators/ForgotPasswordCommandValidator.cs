using Application.Features.Authentication.Commands;
using FluentValidation;

namespace Application.Features.Authentication.Validators
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email address is required.");
        }
    }
}