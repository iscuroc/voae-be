namespace Application.Features.Users.Models
{
    public record struct UserResponse(
        int Id,
        string Names,
        string Lastnames,
        string AccountNumber,
        string Role,
        string Carer
    );
}
