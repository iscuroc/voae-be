using Application.Features.Careers.Extensions;
using Application.Features.Careers.Models;
using Application.Features.Careers.Queries;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Careers.QueryHandlers;

public record GetStudentsByIdQueryHandler(
    ICareerRepository CareerRepository
) : IQueryHandler<GetStudentsByIdQuery, Result<List<CareerUserResponse>>>
{
    public async ValueTask<Result<List<CareerUserResponse>>> Handle(GetStudentsByIdQuery query,
        CancellationToken cancellationToken)
    {
        var users = await CareerRepository.GetStudentsByIdAsync(query.CareerId, query.query, cancellationToken);

        return users.ToResponse();
    }
}