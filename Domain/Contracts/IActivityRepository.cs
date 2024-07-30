using Domain.Entities;

namespace Domain.Contracts;

public interface IActivityRepository
{
    Task AddAsync(Activity activity, CancellationToken cancellationToken = default);
}