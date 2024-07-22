using Application.Contracts;
using Application.Features.Authentication.Commands;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers
{
    public class PasswordResetTwoStepCommandHandler(
        IUserRepository userRepository,
        IUserNotificationService userNotificationService,
        IPasswordHasher passwordHasher
    )
        : ICommandHandler<PasswordResetTwoStepCommand, Result>
    {
        public async ValueTask<Result> Handle(PasswordResetTwoStepCommand request, CancellationToken cancellationToken)
        {
            
            var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null) return Result.Failure(AuthenticationErrors.NotFound);
            
            if (user.PasswordResetToken != request.ResetToken || user.PasswordResetTokenExpiresAt < DateTime.UtcNow)
            {
                return Result.Failure(AuthenticationErrors.TokenExpired);
            }
            
            if (request.Password != request.PasswordConfirmation)
            {
                return Result.Failure(AuthenticationErrors.PasswordDontMatch);
            }
            
            user.Password = passwordHasher.HashPassword(request.Password);
            user.PasswordResetToken = null; 
            user.PasswordResetTokenExpiresAt = null; 

            await userRepository.UpdateAsync(user, cancellationToken);

           
            await userNotificationService.SendPasswordResetConfirmationAsync(user.Email,cancellationToken);

            return Result.Success();
        }
    }
}