using Application.Features.Users.Models;
using Application.Shared;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Users.Queries;

public record GetUsersByRoleQuery(
    UserFilter Filters
) : IQuery<Result<PaginatedList<UsersByRoleResponse>>>;