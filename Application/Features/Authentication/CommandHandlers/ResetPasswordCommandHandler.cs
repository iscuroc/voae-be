using Application.Contracts;
using Application.Features.Authentication.Commands;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers;

public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IUserMailer userMailer,
    IPasswordHasher passwordHasher
)  : ICommandHandler<ResetPasswordCommand, Result>
{
    public async ValueTask<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByResetPasswordTokenAsync(request.ResetToken, cancellationToken);
        if (user is null) return Result.Failure(AuthenticationErrors.InvalidToken);

        if (user.ResetPasswordToken != request.ResetToken)
            return Result.Failure(AuthenticationErrors.InvalidToken);

        if (user.ResetPasswordTokenExpiresAt < DateTime.UtcNow)
            return Result.Failure(AuthenticationErrors.TokenExpired);

        user.ResetPassword(passwordHasher.HashPassword(request.Password));

        await userRepository.UpdateAsync(user, cancellationToken);

        await userMailer.SendResetPasswordConfirmationAsync(user.Email, cancellationToken);

        return Result.Success();
    }
}