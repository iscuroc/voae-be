using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrganizationRepository(ApplicationDbContext context): IOrganizationRepository
{
    public async Task<IEnumerable<Organization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Organizations
            .ToListAsync(cancellationToken);
    }

    public async Task<Organization?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Organizations
            .FindAsync([id], cancellationToken);
    }
};