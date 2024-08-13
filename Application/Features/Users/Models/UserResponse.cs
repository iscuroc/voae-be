using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Users.Models
{
    public record struct UserResponse(
        int Id,
        string Names,
        string Lastnames,
        long AccountNumber,
        Role Role,
        Career? Career
    );
}