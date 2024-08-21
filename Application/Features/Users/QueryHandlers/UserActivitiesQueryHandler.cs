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

        var activitiesResponse = user.JoinedActivities.Select(member => new UserActivitiesResponse(
            Id: member.Activity.Id,
            Name: member.Activity.Name,
            Description: member.Activity.Description,
                Scope: member.Scopes.Select(scope => new ActivitiesScopeResponse(
                    ActivityScopes: scope.MemberScope,
                    Hours: scope.Hours
                )).ToList(),
            StartDate: member.Activity.StartDate,
            EndDate: member.Activity.EndDate,
            Slug: member.Activity.Slug,
            ActivityStatus: member.Activity.ActivityStatus
        )).ToList();

        return Result.Success(activitiesResponse);
    }

}
