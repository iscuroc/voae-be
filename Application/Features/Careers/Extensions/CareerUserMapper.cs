using Application.Features.Careers.Models;
using Domain.Entities;

namespace Application.Features.Careers.Extensions;

public static class CareerUserMapper
{
    public static CareerUserResponse ToResponse(this User user)
    {
        return new CareerUserResponse
        {
            Id = user.Id,
            Names = user.Names!,
            Lastnames = user.Lastnames!,
            Email = user.Email,
            AccountNumber = user.AccountNumber!.Value,
            CareerId = user.CareerId!.Value
        };
    }
    
    public static List<CareerUserResponse> ToResponse(this IEnumerable<User> users)
    {
        return users.Select(user => user.ToResponse()).ToList();
    }
}