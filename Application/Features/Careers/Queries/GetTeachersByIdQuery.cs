using Application.Features.Careers.Models;
using Mediator;
using Shared;

namespace Application.Features.Careers.Queries;

public record GetTeachersByIdQuery(int CareerId) : IQuery<Result<List<TeacherResponse>>>;
