using Domain.Entities;
using Domain.Enums;
using Shared.Pagination;

namespace Domain.Contracts;

public interface IActivityRepository
{
    Task AddAsync(Activity activity, CancellationToken cancellationToken = default);

    Task<IEnumerable<Activity>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string name,
        int careerId,
        ActivityScopes? scope,
        DateTime startDate,
        DateTime endDate,
        ActivityStatus status,
        CancellationToken cancellationToken = default
    );

    Task<long> CountAsync(
        int pageNumber,
        int pageSize,
        string name,
        int careerId,
        ActivityScopes? scope,
        DateTime startDate,
        DateTime endDate,
        ActivityStatus status,
        CancellationToken cancellationToken = default
    );
    
}