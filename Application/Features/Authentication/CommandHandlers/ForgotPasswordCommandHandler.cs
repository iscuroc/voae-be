using Application.Contracts;
using Application.Features.Authentication.Commands;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers
{
    public class ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IUserMailer userMailer
    )
        : ICommandHandler<ForgotPasswordCommand, Result>
    {
        public async ValueTask<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null) return Result.Failure(AuthenticationErrors.NotFound);
            
            var token = Guid.NewGuid().ToString();
            
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddDays(1);
            user.PasswordResetTokenSentAt = DateTime.UtcNow;
            
            await userRepository.UpdateAsync(user, cancellationToken);

            await userMailer.SendForgotPasswordInstructionsAsync(user.Email, token, cancellationToken);

            return Result.Success();
        }
    }
}