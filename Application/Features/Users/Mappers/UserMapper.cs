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
            Career: user.Career
        );
    }
    
    public static List<UserResponse> ToResponse(this IEnumerable<User> users)
    {
        return users.Select(user => user.ToResponse()).ToList();
    }
}
