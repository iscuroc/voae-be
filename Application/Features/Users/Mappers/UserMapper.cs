using Application.Features.Activities.Models;
using Application.Features.Users.Models;
using Application.Shared;
using Domain.Entities;

namespace Application.Features.Users.Mappers;

public static class UserMapper
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(
            Id: user.Id,
            Names: user.Names,
            Lastnames: user.Lastnames!,
            AccountNumber: user.AccountNumber!.Value,
            Role: user.Role,
            Email: user.Email,
            Career: user.CareerId is not null ? user.Career!.ToResponse() : null,
            Organizations: user.Organizations.ToResponse()
        );
    }

    public static List<UserResponse> ToResponse(this IEnumerable<User> users)
    {
        return users.Select(user => user.ToResponse()).ToList();
    }

    public static List<MyActivityResponse> ToRequestResponse(this IEnumerable<Activity> activities)
    {
        return activities.Select(activity => activity.ToMyActivityResponse()).ToList();
    }

    public static UserCareerResponse ToResponse(this Career career)
    {
        return new UserCareerResponse(
            Id: career.Id,
            Name: career.Name
        );
    }

    public static UserOrganizationResponse ToResponse(this Organization organization)
    {
        return new UserOrganizationResponse(
            Id: organization.Id,
            Name: organization.Name
        );
    }

    public static List<UserOrganizationResponse> ToResponse(this IEnumerable<Organization> organizations)
    {
        return organizations.Select(organization => organization.ToResponse()).ToList();
    }
    
    public static ActivityCareerResponse ToActivityResponse(this Career career)
    {
        return new ActivityCareerResponse(
            Id: career.Id,
            Name: career.Name
        );
    }
    
    public static List<ActivityCareerResponse> ToActivityResponse(this IEnumerable<Career> careers)
    {
        return careers.Select(career => career.ToActivityResponse()).ToList();
    }
    
    public static ActivityOrganizationResponse ToActivityResponse(this Organization organization)
    {
        return new ActivityOrganizationResponse(
            Id: organization.Id,
            Name: organization.Name
        );
    }
    
    public static ActivityOrganizerResponse ToResponse(this ActivityOrganizer organizer)
    {
        return new ActivityOrganizerResponse(
            Career: organizer.CareerId is not null ? organizer.Career!.ToActivityResponse() : null,
            Organization: organizer.OrganizationId is not null ? organizer.Organization!.ToActivityResponse() : null
        );
    }
    
    public static List<ActivityOrganizerResponse> ToResponse(this IEnumerable<ActivityOrganizer> organizers)
    {
        return organizers.Select(organizer => organizer.ToResponse()).ToList();
    }
    
    public static ActivityScopeResponse ToResponse(this ActivityScope scope)
    {
        return new ActivityScopeResponse(
            Id: scope.Id,
            Hours: scope.Hours,
            Scope: scope.Scope
        );
    }
    
    public static List<ActivityScopeResponse> ToResponse(this IEnumerable<ActivityScope> scopes)
    {
        return scopes.Select(scope => scope.ToResponse()).ToList();
    }
    
    public static ActivityUserResponse ToActivityResponse(this User user)
    {
        return new ActivityUserResponse(
            Id: user.Id,
            Names: user.Names!,
            Lastnames: user.Lastnames!,
            Role: user.Role
        );
    }

    public static MyActivityResponse ToMyActivityResponse(this Activity activity)
    {
        return new MyActivityResponse(
            Id: activity.Id,
            Slug: activity.Slug,
            Name: activity.Name,
            Description: activity.Description,
            Location: activity.Location,
            MainActivities: activity.MainActivities.MapStringToList(),
            Goals: activity.Goals.MapStringToList(),
            StartDate: activity.StartDate,
            EndDate: activity.EndDate,
            TotalSpots: activity.TotalSpots,
            BannerLink: activity.BannerLink,
            LastRequestedAt: activity.LastRequestedAt,
            ActivityStatus: activity.ActivityStatus,
            LastReviewedAt: activity.LastReviewedAt,
            ReviewObservations: !string.IsNullOrWhiteSpace(activity.ReviewerObservations)
                ? activity.ReviewerObservations.MapStringToList()
                : [],
            Organizers: activity.Organizers.ToResponse(),
            Supervisor: activity.Supervisor.ToActivityResponse(),
            Coordinator: activity.Coordinator.ToActivityResponse(),
            RequestedBy: activity.RequestedBy.ToActivityResponse(),
            ForeingCareers: activity.ForeingCareers.ToActivityResponse(),
            Scopes: activity.Scopes.ToResponse()
        );
    }
}