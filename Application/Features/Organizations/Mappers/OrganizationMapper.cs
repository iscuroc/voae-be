using Application.Features.Organizations.Models;
using Domain.Entities;

namespace Application.Features.Organizations.Mappers;

public static class OrganizationMapper
{
    public static OrganizationResponse ToResponse(this Organization organization)
    {
        return new OrganizationResponse(Id: organization.Id, Name: organization.Name);
    }

    public static List<OrganizationResponse> ToResponse(this IEnumerable<Organization> organizations)
    {
        return organizations.Select(ToResponse).ToList();
    }
}