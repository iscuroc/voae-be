using Application.Features.Careers.Models;
using Shared;

namespace Application.Features.Careers.Queries;

using Mediator;

public record GetStudentsByIdQuery(int CareerId, string? query) : IQuery<Result<List<CareerUserResponse>>>;