using Application.Features.Authentication.Models;
using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands;

public record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>;