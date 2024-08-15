using Application.Features.Users.Models;
using Domain.Entities;

namespace Application.Features.Users.Mappers;

public static class UserMapper
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(
            Id: user.Id,
            Names: user.Names!,
            Lastnames: user.Lastnames!,
            AccountNumber: user.AccountNumber!.Value,
            Role: user.Role,
            Career: user.CareerId is not null ? user.Career!.ToResponse() : null,
            Organizations: user.Organizations.ToResponse()
        );
    }
    
    public static List<UserResponse> ToResponse(this IEnumerable<User> users)
    {
        return users.Select(user => user.ToResponse()).ToList();
    }

    public static UserCareerResponse ToResponse(this Career career)
    {
        return new UserCareerResponse(
            Id: career.Id,
            Name: career.Name
        );
    }
    
    public static UserOrganizationResponse ToResponse(this Organization organization)
    {
        return new UserOrganizationResponse(
            Id: organization.Id,
            Name: organization.Name
        );
    }
    
    public static List<UserOrganizationResponse> ToResponse(this IEnumerable<Organization> organizations)
    {
        return organizations.Select(organization => organization.ToResponse()).ToList();
    }
    
}
