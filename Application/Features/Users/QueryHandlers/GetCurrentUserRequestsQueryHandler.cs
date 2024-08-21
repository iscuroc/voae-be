using Mediator;
using Application.Features.Users.Models;
using Shared;
using Application.Contracts;
using Application.Features.Users.Mappers;
using Application.Features.Activities.Models;
using Application.Features.Activities.Queries;
using Application.Shared;
using Domain.Contracts;
using Application.Features.Activities.Mappers;
using static Domain.Contracts.IActivityRepository;

namespace Application.Features.Users.Queries;

public class GetCurrentUserRequestsQueryHandler(
    IActivityRepository activityRepository
) : IQueryHandler<GetCurrentUserRequestsQuery, Result<PaginatedList<MyActivityResponse>>>
{
    private readonly ICurrentUserService _currentUser;
    public async ValueTask<Result<PaginatedList<MyActivityResponse>>> Handle(GetCurrentUserRequestsQuery query,
        CancellationToken cancellationToken)
    {
        var user = await _currentUser.GetCurrentUserAsync();

        var filters = new UserRequestsFilter
        {
            Id = user.Id,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            Name = query.Name,
            OrganizerCareerId = query.OrganizerCareerId,
            OrganizerOrganizationId = query.OrganizerOrganizationId,
            ForeingCareerId = query.ForeingCareerId,
            Scope = query.Scope,
            StartDateMin = query.StartDateMin,
            StartDateMax = query.StartDateMax,
            EndDateMin = query.EndDateMin,
            EndDateMax = query.EndDateMax,
            Status = query.Status
        };

        var activities = await activityRepository.GetCurrentUserRequestsAsync(
            filters,
            cancellationToken
        );

        var totalActivities = await activityRepository.CountUserRequstsAsync(
            filters,
            cancellationToken
        );

        var activitiesResponse = activities.ToRequestResponse();

        var paginatedList = new PaginatedList<MyActivityResponse>(
            activitiesResponse,
            totalActivities,
            query.PageNumber,
            query.PageSize
        );

        return paginatedList;
    }
}
