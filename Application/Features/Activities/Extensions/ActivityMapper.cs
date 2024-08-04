﻿using Application.Features.Activities.Commands;
using Application.Features.Activities.Models;
using Application.Shared;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Activities.Extensions;

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
            ReviewedAt: activity.ReviewedAt,
            ReviewObservations: activity.ReviewObservations,
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
