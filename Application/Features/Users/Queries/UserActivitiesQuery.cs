using Application.Features.Users.Models;
using Mediator;
using Shared;
using System.Collections.Generic;

namespace Application.Features.Users.Queries;
public record UserActivitiesQuery : IQuery<Result<List<UserActivitiesResponse>>>;

