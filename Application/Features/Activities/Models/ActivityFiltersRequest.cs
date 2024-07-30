using Domain.Enums;

namespace Application.Features.Activities.Models;

public record ActivityFiltersRequest(
    string Name,
    int CareerId,
    ActivityScopes? Scope,
    DateTime StartDate,
    DateTime EndDate,
    ActivityStatus Status
);