using Application.Features.Careers.Extensions;
using Application.Features.Careers.Models;
using Application.Features.Careers.Queries;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Careers.QueryHandlers;

public record GetTeachersByIdQueryHandler(
    ICareerRepository CareerRepository
) : IQueryHandler<GetTeachersByIdQuery, Result<List<TeacherResponse>>>
{
    public async ValueTask<Result<List<TeacherResponse>>> Handle(GetTeachersByIdQuery query,
        CancellationToken cancellationToken)
    {
        var users = await CareerRepository.GetAsync(query.CareerId,cancellationToken);

        return users.ToResponse();
    }
}