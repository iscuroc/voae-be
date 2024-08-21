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
    IActivityRepository ActivityRepository,
    ICurrentUserService CurrentUserService
) : IQueryHandler<UserActivitiesQuery, Result<List<UserActivitiesResponse>>>
{
    private async Task<int> GetCurrentUserIdAsync(CancellationToken cancellationToken)
    {
        var currentUser = await CurrentUserService.GetCurrentUserAsync();
        return currentUser.Id;
    }

    public async ValueTask<Result<List<UserActivitiesResponse>>> Handle(UserActivitiesQuery query, CancellationToken cancellationToken)
    {
        var userId = await GetCurrentUserIdAsync(cancellationToken);
        var user = await UserRepository.GetActivitiesAsync(userId, cancellationToken);

        var activitiesResponse = user.JoinedActivities.Select(activity => new UserActivitiesResponse(
            Id: activity.Activity.Id,
            Name: activity.Activity.Name,
            Description: activity.Activity.Description,
                Scope: activity.Activity.Scopes.Select(scope => new ActivitiesScopeResponse(
                    ActivityScopes: scope.Scope,
                    Hours: scope.Hours
                )).ToList(),
            StartDate: activity.Activity.StartDate,
            EndDate: activity.Activity.EndDate,
            Slug: activity.Activity.Slug,
            ActivityStatus: activity.Activity.ActivityStatus
        )).ToList();

        return Result.Success(activitiesResponse);
    }

}
