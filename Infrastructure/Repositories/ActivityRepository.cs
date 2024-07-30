using Domain.Contracts;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ActivityRepository(ApplicationDbContext context): IActivityRepository
{
    public async Task AddAsync(Activity activity, CancellationToken cancellationToken = default)
    {
        await context.Activities.AddAsync(activity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}