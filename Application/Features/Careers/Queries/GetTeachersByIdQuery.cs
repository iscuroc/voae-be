using Application.Features.Careers.Models;
using Mediator;
using Shared;

namespace Application.Features.Careers.Queries;

public record GetTeachersByIdQuery(
    int CareerId,
    string? Query
) : IQuery<Result<List<CareerUserResponse>>>;
