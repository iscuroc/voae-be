using Application.Features.Users.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Users.Mappers;

public static class UserActivityMapper
{
    public static UserActivitiesResponse ToResponse(this Activity activity)
    {
        return new UserActivitiesResponse(
            Id: activity.Id,
            Name: activity.Name,
            Description: activity.Description,
            Scopes: new ActivitiesScopeResponse{
                ActivityScopes = activity.Scopes.Scope,
                Hours= activity.Scopes.Hours
            }

            StartDate: activity.StartDate,
            EndDate: activity.EndDate,
            Slug: activity.Slug,
            ActivityStatus: activity.ActivityStatus
        );
    }

    public static List<UserActivitiesResponse> ToResponse(this IEnumerable<Activity> activities)
    {
        return activities.Select(ToResponse).ToList();
    }
}