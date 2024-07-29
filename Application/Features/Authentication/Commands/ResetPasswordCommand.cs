using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands
{
    public record ResetPasswordCommand(
        string Password,
        string PasswordConfirmation,
        string ResetToken
    ) : ICommand<Result>;
}