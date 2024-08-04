using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    public async Task AddAsync(Activity activity, CancellationToken cancellationToken = default)
    {
        await context.Activities.AddAsync(activity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Activity>> GetPagedAsync(
        ActivityFilter filters,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.Activities.AsQueryable();
        
        return await query
            .AddIncludes()
            .OrderByDescending(a => a.CreatedAt)
            .ApplyFilters(filters)
            .Page(filters.PageNumber, filters.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(
        ActivityFilter filters,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.Activities.AsQueryable();

        return await query
            .OrderByDescending(a => a.CreatedAt)
            .ApplyFilters(filters)
            .CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await context.Activities.AnyAsync(a => a.Slug == slug, cancellationToken);
    }

    public async Task<Activity?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await context.Activities
            .AddIncludes()
            .FirstOrDefaultAsync(a => a.Slug == slug, cancellationToken);
    }
}