using Application.Features.Activities.Models;
using Mediator;
using Shared;

namespace Application.Features.Activities.Queries;

public record GetActivityBySlugQuery(string Slug) : IQuery<Result<ActivityResponse>>;