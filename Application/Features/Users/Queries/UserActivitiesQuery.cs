using Application.Features.Users.Models;
using Mediator;
using Shared;

namespace Application.Features.Users.Queries;

public record UserActivitiesQuery : IQuery<Result<List<UserActivitiesResponse>>>;