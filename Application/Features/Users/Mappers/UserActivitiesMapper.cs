using Application.Features.Users.Models;
using Domain.Entities;

namespace Application.Features.Users.Mappers;

public static class UserActivityMapper
{
    public static UserActivitiesResponse ToResponse(this Activity activity)
    {
        return new UserActivitiesResponse(
            Id: activity.Id,
            Name: activity.Name,
            Description: activity.Description,
            StartDate: activity.StartDate,
            EndDate: activity.EndDate,
            ActivityStatus: activity.ActivityStatus.ToString()
        );
    }

    public static List<UserActivitiesResponse> ToResponse(this IEnumerable<Activity> activities)
    {
        return activities.Select(ToResponse).ToList();
    }
}