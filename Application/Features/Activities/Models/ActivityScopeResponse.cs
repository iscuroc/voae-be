using Domain.Enums;

namespace Application.Features.Activities.Models;

public record ActivityScopeResponse(
    int HourAmount,
    ActivityScopes Scope 
);