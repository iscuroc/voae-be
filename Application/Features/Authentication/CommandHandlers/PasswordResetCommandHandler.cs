using Application.Contracts;
using Application.Features.Authentication.Commands;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Authentication.CommandHandlers
{
    public class ResetPasswordCommandHandler(
        IUserNotificationService userNotificationService,
        IUserRepository userRepository
    )
        : ICommandHandler<ResetPasswordCommand, Result>
    {
        public async ValueTask<Result> Handle(ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null) return Result.Failure(AuthenticationErrors.InvalidAccountNumber);

            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddHours(1);

            await userRepository.UpdateAsync(user, cancellationToken);

            await userNotificationService.SendResetPasswordInstructionsAsync(user.Email, token, cancellationToken);
            
            return Result.Success();
        }
    }
}