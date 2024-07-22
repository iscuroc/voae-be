using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands
{
    public record PasswordResetTwoStepCommand(
        string Email,
        string Password,
        string PasswordConfirmation,
        string ResetToken
    ) : ICommand<Result>;
}