using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class UserExtensions
{
    public static IQueryable<User> ByLookupField(
        this IQueryable<User> query,
        string? queryFilter
    )
    {
        if (string.IsNullOrWhiteSpace(queryFilter)) return query;

        var lookupFilter = queryFilter.ToLower();

        query = query.Where(u =>
            (u.Names != null && u.Names.ToLower().Contains(lookupFilter)) ||
            (u.Lastnames != null && u.Lastnames.ToLower().Contains(lookupFilter)) ||
            u.Email.ToLower().Contains(lookupFilter) ||
            u.AccountNumber != null && u.AccountNumber.ToString()!.Contains(lookupFilter)
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

    public static IQueryable<User> ApplyFilters(
        this IQueryable<User> query,
        UserFilter filters
    )
    {
        query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.Query))
            //so a filter by name and email
            query = query.Where(u =>
                (u.Names != null && u.Names.Contains(filters.Query)) ||
                (u.Lastnames != null && u.Lastnames.Contains(filters.Query)) ||
                u.Email.Contains(filters.Query) ||
                u.AccountNumber.ToString()!.Contains(filters.Query)
            );

        if (filters.Role?.Length > 0)
            query = query.Where(u => filters.Role.Contains(u.Role));


        return query;
    }
}