using Domain.Entities;

namespace Domain.Contracts;

public interface IOrganizationRepository
{
    Task<IEnumerable<Organization>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Organization?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
};