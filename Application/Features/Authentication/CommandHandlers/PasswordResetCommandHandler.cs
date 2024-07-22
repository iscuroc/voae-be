using Application.Contracts;
using Application.Features.Authentication.Commands;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers
{
    public class PasswordResetCommandHandler(
        IUserRepository userRepository,
        IUserNotificationService userNotificationService
    )
        : ICommandHandler<PasswordResetCommand, Result>
    {
        public async ValueTask<Result> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            // Fetch the user by email
            var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null) return Result.Failure(AuthenticationErrors.NotFound);

            // Generate and set the reset token and its expiration
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddHours(1);

            // Update the user with the new reset token
            await userRepository.UpdateAsync(user, cancellationToken);

            // Send the reset password instructions to the user
            await userNotificationService.SendResetPasswordInstructionsAsync(user.Email, token, cancellationToken);

            return Result.Success();
        }
    }
}