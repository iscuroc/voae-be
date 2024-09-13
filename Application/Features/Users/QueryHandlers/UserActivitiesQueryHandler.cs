using Application.Contracts;
using Application.Features.Users.Mappers;
using Application.Features.Users.Models;
using Application.Features.Users.Queries;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Users.QueryHandlers;

public record UserActivitiesQueryHandler(
    IUserRepository UserRepository,
    ICurrentUserService CurrentUserService
) : IQueryHandler<UserActivitiesQuery, Result<List<UserActivitiesResponse>>>
{
    public async ValueTask<Result<List<UserActivitiesResponse>>> Handle(UserActivitiesQuery query,
        CancellationToken cancellationToken)
    {
        var user = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
        var activityMembers = await UserRepository.GetMyActivitiesAsync(user.Id, cancellationToken);

        return activityMembers.ToResponse();
    }
}