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
            
            user.GenerateResetPasswordToken();
            
            await userRepository.UpdateAsync(user, cancellationToken);

            await userMailer.SendForgotPasswordInstructionsAsync(
                user.Email, 
                user.ResetPasswordToken!, 
                cancellationToken
            );

            return Result.Success();
        }
    }
}