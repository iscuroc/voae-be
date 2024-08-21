using Application.Features.Activities.Models;
using Application.Features.Users.Models;
using Application.Shared;
using Domain.Enums;
using Mediator;
using Shared;

namespace Application.Features.Users.Queries;

public record GetCurrentUserRequestsQuery(
    string? Name,
    int? OrganizerCareerId,
    int? OrganizerOrganizationId,
    int? ForeingCareerId,
    ActivityScopes? Scope,
    DateTime? StartDateMin,
    DateTime? StartDateMax,
    DateTime? EndDateMin,
    DateTime? EndDateMax,
    ActivityStatus? Status
) : PaginationBase, IQuery<Result<PaginatedList<MyActivityResponse>>>;