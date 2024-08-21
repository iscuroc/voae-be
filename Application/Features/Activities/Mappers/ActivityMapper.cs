using Application.Features.Activities.Commands;
using Application.Features.Activities.Models;
using Application.Shared;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Activities.Mappers;

public static class ActivityMapper
{
    public static ActivityResponse ToResponse(this Activity activity)
    {
        return new ActivityResponse(
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
            Supervisor: activity.Supervisor.ToResponse(),
            Coordinator: activity.Coordinator.ToResponse(),
            RequestedBy: activity.RequestedBy.ToResponse(),
            ForeingCareers: activity.ForeingCareers.ToResponse(),
            Scopes: activity.Scopes.ToResponse()
        );
    }

    public static List<ActivityResponse> ToResponse(this IEnumerable<Activity> activities)
    {
        return activities.Select(activity => activity.ToResponse()).ToList();
    }

    public static Activity ToEntity(
        this CreateActivityCommand request,
        ActivityStatus status,
        List<Career> foreignCareers,
        List<ActivityOrganizer> organizers,
        int requestedById
    )
    {
        var scopes = request.Scopes.Select(scope => new ActivityScope
        {
            Scope = scope.Scope,
            Hours = scope.Hours
        }).ToList();

        var activity = new Activity
        {
            Name = request.Name,
            Slug = request.Name.Slugify(),
            Description = request.Description,
            Goals = request.Goals.MapListToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalSpots = request.TotalSpots,
            SupervisorId = request.SupervisorId,
            CoordinatorId = request.CoordinatorId,
            Location = request.Location,
            ActivityStatus = status,
            MainActivities = request.MainActivities.MapListToString(),
            Scopes = scopes,
            ForeingCareers = foreignCareers,
            RequestedById = requestedById,
            LastRequestedAt = DateTime.UtcNow,
            Organizers = organizers
        };

        return activity;
    }
}