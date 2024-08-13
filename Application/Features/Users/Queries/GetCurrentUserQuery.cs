using Application.Features.Users.Models;
using Mediator;
using Shared;

namespace Application.Features.Users.Queries;

public record GetCurrentUserQuery(
    int UserId,
    string? Query
) : IQuery<Result<List<UserResponse>>>;