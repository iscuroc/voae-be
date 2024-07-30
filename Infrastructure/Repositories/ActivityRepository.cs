using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    public async Task AddAsync(Activity activity, CancellationToken cancellationToken = default)
    {
        await context.Activities.AddAsync(activity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Activity>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string name,
        int careerId,
        ActivityScopes? scope,
        DateTime startDate,
        DateTime endDate,
        ActivityStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.Activities.AsNoTracking();
        var filteredQuery = CreateFilter(query, name, careerId, scope, startDate, endDate, status);

        return dataPagedList = await filteredQuery
            .OrderBy(a => a.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(
        int pageNumber,
        int pageSize,
        string name,
        int careerId,
        ActivityScopes? scope,
        DateTime startDate,
        DateTime endDate,
        ActivityStatus status,
        CancellationToken cancellationToken = default
    )
    {
 
        var query = context.Activities.AsNoTracking();
        var filteredQuery = CreateFilter(query, name, careerId, scope, startDate, endDate, status);

        return await filteredQuery.CountAsync(cancellationToken);
    }

    private void CreateFilter(IQueryable<Activity> query,
        string name,
        int careerId,
        ActivityScopes? scope,
        DateTime startDate,
        DateTime endDate,
        ActivityStatus status
    )
    {
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(a => a.Name.Contains(name));

        if (careerId > 0)
            query = query.Where(a => a.MainCareerId == careerId);

        if (scope is not null)
            query = query.Where(a => a.Scopes.Any(s => s.Scope == scope));

        if (startDate != default)
            query = query.Where(a => a.StartDate >= startDate);

        if (endDate != default)
            query = query.Where(a => a.EndDate <= endDate);

        if (status != default)
            query = query.Where(a => a.ActivityStatus == status);

        return query;
    }

}