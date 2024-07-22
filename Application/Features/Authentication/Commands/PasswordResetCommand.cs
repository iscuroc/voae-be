using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands
{
    public record PasswordResetCommand(
        string Email,
        string Password,
        string PasswordConfirmation,
        string ResetToken
    ) : ICommand<Result>;
}