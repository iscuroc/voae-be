using Application.Features.Careers.Mappers;
using Application.Features.Careers.Models;
using Application.Features.Careers.Queries;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Careers.QueryHandlers;

public record GetTeachersByIdQueryHandler(
    ICareerRepository CareerRepository
) : IQueryHandler<GetTeachersByIdQuery, Result<List<CareerUserResponse>>>
{
    public async ValueTask<Result<List<CareerUserResponse>>> Handle(GetTeachersByIdQuery query,
        CancellationToken cancellationToken)
    {
        var users = await CareerRepository.GetTeachersByIdAsync(query.CareerId, query.Query, cancellationToken);

        return users.ToResponse();
    }
}