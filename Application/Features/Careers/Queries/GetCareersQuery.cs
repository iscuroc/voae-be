using Application.Features.Careers.Models;
using Mediator;
using Shared;

namespace Application.Features.Careers.Queries;

public record GetCareersQuery : IQuery<Result<List<CareerResponse>>>;