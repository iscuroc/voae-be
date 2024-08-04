using Application.Features.Organizations.Models;
using Mediator;
using Shared;

namespace Application.Features.Organizations.Queries;

public record GetOrganizationsQuery : IQuery<Result<List<OrganizationResponse>>>;