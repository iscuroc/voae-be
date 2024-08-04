using Domain.Entities;

namespace Domain.Contracts;

public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

};