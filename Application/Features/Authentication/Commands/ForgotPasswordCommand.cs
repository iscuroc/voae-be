using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands
{
    public record ForgotPasswordCommand(
        string Email
    ) : ICommand<Result>;
}