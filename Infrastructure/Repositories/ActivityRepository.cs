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
            .Include(a => a.Supervisor)
            .Include(a => a.Coordinator)
            .Include(a => a.Organizers)
            .ThenInclude(o => o.Organization)
            .Include(a => a.Organizers)
            .ThenInclude(o => o.Career)
            .Include(a => a.ForeingCareers)
            .Include(a => a.Scopes)
            .Include(a => a.RequestedBy)
            .Include(a => a.ReviewedBy)
            .ApplyFilters(filters)
            .OrderByDescending(a => a.CreatedAt)
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
            .ApplyFilters(filters)
            .OrderByDescending(a => a.CreatedAt)
            .CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await context.Activities.AnyAsync(a => a.Slug == slug, cancellationToken);
    }
}