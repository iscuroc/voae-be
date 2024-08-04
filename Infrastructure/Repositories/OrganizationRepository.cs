using Domain.Contracts;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class OrganizationRepository(ApplicationDbContext context): IOrganizationRepository
{
    public async Task<Organization?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Organizations
            .FindAsync([id], cancellationToken);
    }
};