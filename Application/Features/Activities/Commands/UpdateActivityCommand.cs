using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;

public record UpdateActivityCommand(
    int Id,
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
    
) : ICommand<Result>;