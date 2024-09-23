using Application.Contracts;
using Application.Features.Authentication.Commands;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Authentication.CommandHandlers;

public class ConfirmUserCommandHandler(
    IUserRepository userRepository,
    IUserMailer userMailer,
    IPasswordHasher passwordHasher,
    ICareerRepository careerRepository,
    IOrganizationRepository organizationRepository
) : ICommandHandler<ConfirmUserCommand, Result>
{
    public async ValueTask<Result> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByConfirmationTokenAsync(request.EmailConfirmationToken, cancellationToken);
        if (user is null) return Result.Failure(AuthenticationErrors.InvalidToken);

        if (user.EmailConfirmedAt is not null) 
            return Result.Failure(AuthenticationErrors.EmailAlreadyConfirmed);

        if (user.EmailConfirmationToken != request.EmailConfirmationToken)
            return Result.Failure(AuthenticationErrors.InvalidToken);

        if (user.EmailConfirmationTokenExpiresAt < DateTime.UtcNow)
            return Result.Failure(AuthenticationErrors.TokenExpired);

        user.ConfirmEmail();

        user.Names = request.Names;
        user.Lastnames = request.Lastnames;
        user.AccountNumber = request.AccountNumber;
        user.Password = passwordHasher.HashPassword(request.Password);

        user.SetRoleByEmail();

        if (!user.IsAccoutNumberValid()) return Result.Failure(AuthenticationErrors.InvalidAccountNumber);

        var anotherAccountExists =
            await userRepository.AccoutNumberExistsAsync(request.AccountNumber, cancellationToken);
        if (anotherAccountExists) return Result.Failure(AuthenticationErrors.AccountNumberInUse);

        var careerExists = await careerRepository.ExistsAsync(request.CareerId, cancellationToken);
        if (!careerExists) return Result.Failure(CareerErrors.CareerNotFound);
        
        user.CareerId = request.CareerId;
        
        user.Organizations.Clear();
        foreach (var organizationId in request?.OrganizationIds ?? new List<int>())
        {
            var organization = await organizationRepository.GetByIdAsync(organizationId, cancellationToken);
            if (organization is null) return Result.Failure(OrganizationErrors.OrganizationNotFound);
            
            user.Organizations.Add(organization);
        }

        await userRepository.UpdateAsync(user, cancellationToken);

        await userMailer.SendWelcomeEmailAsync(user.Email, cancellationToken);

        return Result.Success();
    }
}