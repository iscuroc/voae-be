using Application.Features.Users.Models;
using FluentValidation;

namespace Application.Features.Users.Validators;

public class UserValidator : AbstractValidator<UserResponse>
{
    public UserValidator()
    {
        RuleFor(x => x.Names)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(x => x.Lastnames)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(x => x.AccountNumber)
            .GreaterThan(0);

        RuleFor(x => x.Role)
            .IsInEnum();

        RuleFor(x => x.UserCareer)
            .SetValidator(new UserCareerValidator());
    }
}

public class UserCareerValidator : AbstractValidator<UserCareerResponse>
{
    public UserCareerValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
    }
}
