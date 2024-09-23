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
    List<string> ReviewObservations,
    List<ActivityOrganizerResponse> Organizers,
    ActivityUserResponse Supervisor,
    ActivityUserResponse Coordinator,
    ActivityUserResponse RequestedBy,
    List<ActivityCareerResponse> ForeingCareers,
    List<ActivityScopeResponse> Scopes);

public record ActivityResponseWithMembers(
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
    List<string> ReviewObservations,
    List<ActivityOrganizerResponse> Organizers,
    ActivityUserResponse Supervisor,
    ActivityUserResponse Coordinator,
    ActivityUserResponse RequestedBy,
    List<ActivityCareerResponse> ForeingCareers,
    List<ActivityScopeResponse> Scopes,
    List<ActivityMemberResponse> Members
);

public record ActivityScopeResponse(
    int Id,
    int Hours,
    ActivityScopes Scope
);

public record ActivityMemberResponse(
    int Id,
    string? Names,
    string Lastnames,
    long? Account,
    string Career,
    IEnumerable<ActivityScopes> Scopes
);

public record ActivityUserResponse(
    int Id,
    string Names,
    string Lastnames,
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