using Application.Features.Careers.Mappers;
using Application.Features.Careers.Models;
using Application.Features.Careers.Queries;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Careers.QueryHandlers;

public record GetCareersQueryHandler(
    ICareerRepository CareerRepository
) : IQueryHandler<GetCareersQuery, Result<List<CareerResponse>>>
{
    public async ValueTask<Result<List<CareerResponse>>> Handle(GetCareersQuery query,
        CancellationToken cancellationToken)
    {
        var careers = await CareerRepository.GetAllAsync(cancellationToken);

        return careers.ToResponse();
    }
}