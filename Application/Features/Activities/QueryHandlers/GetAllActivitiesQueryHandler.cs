using Application.Features.Activities.Models;
using Application.Features.Activities.Queries;
using Application.Features.Authentication.Models;
using Domain.Contracts;
using Mediator;
using Shared;
using Shared.Pagination;

namespace Application.Features.Activities.QueryHandlers;

public class GetAllActivitiesQueryHandler(IActivityRepository activityRepository)
    : IQueryHandler<GetAllActivitiesQuery, Result<PagedList<ActivityResponse>>>
{
    public async ValueTask<Result<PagedList<ActivityResponse>>> Handle(GetAllActivitiesQuery query,
        CancellationToken cancellationToken)
    {
        var activities = await activityRepository.GetAllAsync(
            query.Pagination,
            query.ActivityFiltersRequest.Name,
            query.ActivityFiltersRequest.CareerId,
            query.ActivityFiltersRequest.Scope,
            query.ActivityFiltersRequest.StartDate,
            query.ActivityFiltersRequest.EndDate,
            query.ActivityFiltersRequest.Status,
            cancellationToken
        );

        var activitiesResponse = activities.Select(a => new ActivityResponse(
            a.Id,
            a.Name,
            a.Description,
            a.Location,
            a.MainActivities,
            a.Objectives,
            a.MainCareer,
            a.StartDate,
            a.EndDate,
            a.TotalSpots,
            a.BannerLink,
            a.Scopes.Select(s => new ActivityScopeResponse(s.HourAmount, s.Scope)).ToList(),
            a.ForeingCareers.Select(c => new CareerResponse(c.Id, c.Name)).ToList(),
            a.TeacherId,
            new UserResponse(a.Teacher.Id, a.Teacher.Names!, a.Teacher.Email, a.Teacher.Role),
            new UserResponse(a.Student.Id, a.Student.Names!, a.Student.Email, a.Student.Role),
            a.ActivityStatus,
            a.RequestedAt,
            a.ReviewDate
        ));

        var nactivities = new PagedList<ActivityResponse>(
            activitiesResponse,
            activities.TotalCount,
            activities.CurrentPage,
            activities.PageSize
        );
        return Result.Success(nactivities);
    }
}