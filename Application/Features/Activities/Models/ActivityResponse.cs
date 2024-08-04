using Domain.Enums;

namespace Application.Features.Activities.Models;

public record ActivityResponse(
    int Id,
    string Slug,
    string Name,
    string Description,
    string Location,
    List<string> MainActivities,
    List<string> Goals,
    DateTime StartDate,
    DateTime EndDate,
    int TotalSpots,
    string? BannerLink,
    DateTime LastRequestedAt,
    ActivityStatus ActivityStatus,
    DateTime? LastReviewedAt,
    string? ReviewObservations,
    List<ActivityOrganizerResponse> Organizers,
    ActivityUserResponse Supervisor,
    ActivityUserResponse Coordinator,
    ActivityUserResponse RequestedBy,
    List<ActivityCareerResponse> ForeingCareers,
    List<ActivityScopeResponse> Scopes
);

public record ActivityScopeResponse(
    int Id,
    int HourAmount,
    ActivityScopes Scope
);

public record ActivityUserResponse(
    int Id,
    string Names,
    string LastNames,
    Role Role
);

public record ActivityCareerResponse(
    int Id,
    string Name
);

public record ActivityOrganizationResponse(
    int Id,
    string Name
);

public record ActivityOrganizerResponse(
    ActivityCareerResponse? Career,
    ActivityOrganizationResponse? Organization
);