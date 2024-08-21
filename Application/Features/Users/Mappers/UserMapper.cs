using Application.Features.Activities.Mappers;
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
            Names: user.Names!,
            Lastnames: user.Lastnames!,
            AccountNumber: user.AccountNumber!.Value,
            Role: user.Role,
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
            ReviewObservations: activity.ReviewerObservations.MapStringToList(),
            Organizers: activity.Organizers.ToResponse(),
            Supervisor: activity.Supervisor.ToResponse(),
            Coordinator: activity.Coordinator.ToResponse(),
            RequestedBy: activity.RequestedBy.ToResponse(),
            ForeingCareers: activity.ForeingCareers.ToResponse(),
            Scopes: activity.Scopes.ToResponse()

        );
    }

}
