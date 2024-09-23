using Mediator;
using Shared;

namespace Application.Features.Authentication.Commands;

public record ConfirmUserCommand(
    string Names,
    string Lastnames,
    long AccountNumber,
    string Password,
    int CareerId,
    List<int>? OrganizationIds,
    string PasswordConfirmation,
    string EmailConfirmationToken
) : ICommand<Result>;