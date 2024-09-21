using Application.Contracts;
using Application.Features.Authentication.Commands;
using Application.Features.Authentication.Models;
using Domain.Contracts;
using Domain.Entities;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers;

public class RegisterUserCommandHandler(
    IUserMailer userMailer,
    IUserRepository userRepository
) : ICommandHandler<RegisterUserCommand, Result>
{
    public async ValueTask<Result> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var emailExist = await userRepository.EmailExistsAsync(request.Email, cancellationToken);
        if (emailExist) return Result.Failure<TokenResponse>(AuthenticationErrors.EmailInUse);
        
        var user = new User
        {
            Email = request.Email
        };
        
        user.GenerateConfirmationToken();

        await userRepository.AddAsync(user, cancellationToken);

        await userMailer.SendConfirmationInstructionsAsync(
            user.Email, 
            user.EmailConfirmationToken!, 
            cancellationToken
        );
        Console.WriteLine(user.EmailConfirmationToken);
        return Result.Success();
    }
}