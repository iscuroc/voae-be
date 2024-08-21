using Application.Features.Activities.Commands;

namespace Application.Features.Activities.Models;

public record UpdateActivityRequest(
    string Name,
    string Description,
    List<int> ForeignCareersIds,
    DateTime StartDate,
    DateTime EndDate,
    List<string> Goals,
    List<ActivityScopeRequest> Scopes,
    int SupervisorId,
    int CoordinatorId,
    int TotalSpots,
    string Location,
    List<string> MainActivities,
    List<ActivityOrganizerRequest> Organizers
);