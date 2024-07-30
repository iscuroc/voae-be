using Application.Features.Activities.Models;
using Mediator;
using Shared;
using Shared.Pagination;

namespace Application.Features.Activities.Queries;

public record GetAllActivitiesQuery(PaginationOptions Pagination, ActivityFiltersRequest ActivityFiltersRequest)
    : IQuery<Result<PagedList<ActivityResponse>>>;