using Application.Features.Activities.Models;
using Domain.Entities;

namespace Application.Features.Activities.Mappers;

public static class ActivityRelationsMapper
{
    public static ActivityScopeResponse ToResponse(this ActivityScope activityScope)
    {
        return new ActivityScopeResponse(
            Id: activityScope.Id,
            HourAmount: activityScope.Hours,
            Scope: activityScope.Scope
        );
    }

    public static List<ActivityScopeResponse> ToResponse(this IEnumerable<ActivityScope> activityScopes)
    {
        return activityScopes.Select(activityScope => activityScope.ToResponse()).ToList();
    }

    public static ActivityCareerResponse ToResponse(this Career career)
    {
        return new ActivityCareerResponse(
            Id: career.Id,
            Name: career.Name
        );
    }

    public static List<ActivityCareerResponse> ToResponse(this IEnumerable<Career> careers)
    {
        return careers.Select(career => career.ToResponse()).ToList();
    }

    public static ActivityUserResponse ToResponse(this User user)
    {
        return new ActivityUserResponse(
            Id: user.Id,
            Names: user.Names!,
            LastNames: user.Lastnames!,
            Role: user.Role
        );
    }

    public static ActivityOrganizationResponse ToResponse(this Organization organization)
    {
        return new ActivityOrganizationResponse(
            Id: organization.Id,
            Name: organization.Name
        );
    }

    public static ActivityOrganizerResponse ToResponse(this ActivityOrganizer organizer)
    {
        return new ActivityOrganizerResponse(
            Career: organizer.Career?.ToResponse(),
            Organization: organizer.Organization?.ToResponse()
        );
    }

    public static List<ActivityOrganizerResponse> ToResponse(this IEnumerable<ActivityOrganizer> organizers)
    {
        return organizers.Select(organizer => organizer.ToResponse()).ToList();
    }
}