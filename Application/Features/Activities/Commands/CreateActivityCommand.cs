using Domain.Enums;
using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;

public record CreateActivityCommand(
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

public record ActivityScopeRequest(
    ActivityScopes Scope,
    int Hours
);

public record ActivityOrganizerRequest(
    int? CareerId,
    int? OrganizationId,
    OrganizerType Type
);

public enum OrganizerType
{
    Career,
    Organization
}