using Application.Contracts;
using Application.Features.Authentication.Commands;
using Application.Features.Authentication.Models;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers;

public record LoginCommandHandler(
    IUserRepository UserRepository,
    IJwtProvider Jwt,
    IPasswordHasher PasswordHasher
) : ICommandHandler<LoginCommand, Result<TokenResponse>>
{
    public async ValueTask<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await UserRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null) return Result.Failure<TokenResponse>(AuthenticationErrors.InvalidCredentials);
        
        var isPasswordValid = PasswordHasher.VerifyPassword(request.Password, user.Password!);
        if (!isPasswordValid) return Result.Failure<TokenResponse>(AuthenticationErrors.InvalidCredentials);
        
        var accessToken = Jwt.GenerateToken(user.Id, user.Email);

        return new TokenResponse(user.Email, user.Role, accessToken);
    }
}