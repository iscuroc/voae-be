using Application.Features.Activities.Mappers;
using Application.Features.Activities.Models;
using Application.Features.Activities.Queries;
using Domain.Contracts;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.QueryHandlers;

public record GetActivityBySlugQueryHandler(
    IActivityRepository ActivityRepository
) : IQueryHandler<GetActivityBySlugQuery, Result<ActivityResponse>>
{
    public async ValueTask<Result<ActivityResponse>> Handle(GetActivityBySlugQuery query, CancellationToken cancellationToken)
    {
        var activity = await ActivityRepository.GetBySlugAsync(query.Slug, cancellationToken);

        return activity?.ToResponse() ?? Result.Failure<ActivityResponse>(ActivityErrors.ActivitySlugNotFound);
    }
}