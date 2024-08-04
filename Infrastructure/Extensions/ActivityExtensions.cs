using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class ActivityExtensions
{
    public static IQueryable<Activity> ApplyFilters(
        this IQueryable<Activity> query,
        ActivityFilter filters
    )
    {
        query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.Name))
            query = query.Where(a => a.Name.Contains(filters.Name));

        if (filters.OrganizerCareerId.HasValue)
            query = query.Where(a => a.Organizers.Any(o => o.CareerId == filters.OrganizerCareerId));
        
        if (filters.OrganizerOrganizationId.HasValue)
            query = query.Where(a => a.Organizers.Any(o => o.OrganizationId == filters.OrganizerOrganizationId));
        
        if (filters.ForeingCareerId.HasValue)
            query = query.Where(a => a.ForeingCareers.Any(fc => fc.Id == filters.ForeingCareerId));

        if (filters.Scope.HasValue)
            query = query.Where(a => a.Scopes.Any(s => s.Scope == filters.Scope));

        if (filters is {StartDateMin: not null, StartDateMax: not null})
            query = query.Where(a => a.StartDate >= filters.StartDateMin && a.StartDate <= filters.StartDateMax);
        
        if (filters is {StartDateMin: not null, StartDateMax: null})
            query = query.Where(a => a.StartDate >= filters.StartDateMin);
        
        if (filters is {StartDateMin: null, StartDateMax: not null})
            query = query.Where(a => a.StartDate <= filters.StartDateMax);
        
        if (filters is {EndDateMin: not null, EndDateMax: not null})
            query = query.Where(a => a.EndDate >= filters.EndDateMin && a.EndDate <= filters.EndDateMax);
        
        if (filters is {EndDateMin: not null, EndDateMax: null})
            query = query.Where(a => a.EndDate >= filters.EndDateMin);
        
        if (filters is {EndDateMin: null, EndDateMax: not null})
            query = query.Where(a => a.EndDate <= filters.EndDateMax);

        if (filters.Status.HasValue)
            query = query.Where(a => a.ActivityStatus == filters.Status);

        return query;
    }
}