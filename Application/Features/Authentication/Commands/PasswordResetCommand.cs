using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands
{
    public record PasswordResetCommand(
        string Email
    ) : ICommand<Result>;
}