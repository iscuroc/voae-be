using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Extensions;

public static class UserExtensions
{
    public static IQueryable<User> ByLookupField(
        this IQueryable<User> query,
        string? queryFilter
    )
    {
        if (string.IsNullOrWhiteSpace(queryFilter)) return query;
        
        query = query.Where(u =>
            (u.Names != null && u.Names.Contains(queryFilter)) ||
            (u.Lastnames != null && u.Lastnames.Contains(queryFilter)) ||
            u.Email.Contains(queryFilter) ||
            u.AccountNumber != null && u.AccountNumber.ToString()!.Contains(queryFilter)
        );
        
        return query;
    }
    
    public static IQueryable<User> ConfirmedOnly(this IQueryable<User> query)
    {
        return query.Where(u => u.EmailConfirmedAt.HasValue);
    }
    
    public static IQueryable<User> ByRole(this IQueryable<User> query, Role role)
    {
        return query.Where(u => u.Role == role);
    }
    
    public static IQueryable<User> ByCareer(this IQueryable<User> query, int careerId)
    {
        return query.Where(u => u.CareerId == careerId);
    }
} 