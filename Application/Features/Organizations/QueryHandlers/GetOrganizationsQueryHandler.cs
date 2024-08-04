using Application.Features.Organizations.Mappers;
using Application.Features.Organizations.Models;
using Application.Features.Organizations.Queries;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Organizations.QueryHandlers;

public record GetOrganizationsQueryHandler(
    IOrganizationRepository OrganizationRepository
) : IQueryHandler<GetOrganizationsQuery, Result<List<OrganizationResponse>>>
{
    public async ValueTask<Result<List<OrganizationResponse>>> Handle(GetOrganizationsQuery query, CancellationToken cancellationToken)
    {
        var organizations = await OrganizationRepository.GetAllAsync(cancellationToken);

        return organizations.ToResponse();
    }
}