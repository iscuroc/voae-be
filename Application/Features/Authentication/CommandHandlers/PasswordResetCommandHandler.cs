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
            var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null) return Result.Failure(AuthenticationErrors.NotFound);
            
            var token = Guid.NewGuid().ToString();

            await userNotificationService.SendResetPasswordInstructionsAsync(user.Email, token, cancellationToken);

            return Result.Success();
        }
    }
}