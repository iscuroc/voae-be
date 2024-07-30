using Domain.Entities;
using Domain.Enums;
using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;

public record CreateActivityCommand(
    string Name,
    string Description,
    int MainCareerId,
    List<int> AvailableCareers,
    DateTime StartDate,
    DateTime EndDate,
    string Goals,
    List<ActivityScope> Scopes,
    int CareerTeacherId,
    int CareerStudentId,
    int TotalSpots,
    string Location,
    IList<string> MainActivities
    
) : ICommand<Result>;

public record ActivityScope(
    ActivityScopes Scope,
    int Hours
);