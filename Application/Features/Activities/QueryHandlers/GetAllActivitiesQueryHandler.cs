using Application.Features.Activities.Extensions;
using Application.Features.Activities.Models;
using Application.Features.Activities.Queries;
using Application.Shared;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Activities.QueryHandlers;

public class GetAllActivitiesQueryHandler(
    IActivityRepository activityRepository
) : IQueryHandler<GetAllActivitiesQuery, Result<PaginatedList<ActivityResponse>>>
{
    public async ValueTask<Result<PaginatedList<ActivityResponse>>> Handle(GetAllActivitiesQuery query,
        CancellationToken cancellationToken)
    {
        var filters = new ActivityFilter
        {
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
        
        var activities = await activityRepository.GetPagedAsync(
            filters,
            cancellationToken
        );
        
        var totalActivities = await activityRepository.CountAsync(
            filters,
            cancellationToken
        );

        var activitiesResponse = activities.ToResponse();

        var paginatedList = new PaginatedList<ActivityResponse>(
            activitiesResponse,
            totalActivities,
            query.PageNumber,
            query.PageSize
        );
        
        return paginatedList;
    }
}