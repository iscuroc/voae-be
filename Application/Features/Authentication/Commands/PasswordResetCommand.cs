using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands;

public record ResetPasswordCommand(string Email) : ICommand<Result>;