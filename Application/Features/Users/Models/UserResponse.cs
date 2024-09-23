using Domain.Enums;

namespace Application.Features.Users.Models
{
    public record struct UserResponse(
        int Id,
        string Names,
        string Lastnames,
        long AccountNumber,
        Role Role,
        string Email,
        UserCareerResponse? Career,
        List<UserOrganizationResponse> Organizations
    );

    public record struct UserCareerResponse(
        int Id,
        string Name
    );
    
    public record struct UserOrganizationResponse(
        int Id,
        string Name
    );
}