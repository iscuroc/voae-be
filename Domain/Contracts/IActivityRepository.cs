using Domain.Entities;
using Domain.Enums;
using Shared;

namespace Domain.Contracts;

public interface IActivityRepository
{
    Task AddAsync(Activity activity, CancellationToken cancellationToken = default);

    Task<IEnumerable<Activity>> GetPagedAsync(
        ActivityFilter filters,
        CancellationToken cancellationToken = default
    );

    Task<long> CountAsync(
        ActivityFilter filters,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Activity?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Activity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Activity activity, CancellationToken cancellationToken);
    Task AddMemberAsync(ActivityMember activityMember, CancellationToken cancellationToken);

    Task<IEnumerable<Activity>> GetMyRequestsAsync(
        UserRequestsFilter filters,
        CancellationToken cancellationToken = default
    );

    Task<long> CountMyRequestsAsync(
        UserRequestsFilter filters,
        CancellationToken cancellationToken = default
    );
}

public record ActivityFilter : PaginationBase
{
    public required string? Name { get; set; }
    public required int? OrganizerCareerId { get; set; }
    public required int? OrganizerOrganizationId { get; set; }
    public required int? ForeingCareerId { get; set; }
    public required ActivityScopes? Scope { get; set; }
    public required DateTime? StartDateMin { get; set; }
    public required DateTime? StartDateMax { get; set; }
    public required DateTime? EndDateMin { get; set; }
    public required DateTime? EndDateMax { get; set; }
    public required ActivityStatus? Status { get; set; }
}

public record UserRequestsFilter : PaginationBase
{
    public required int? Id { get; set; }
    public required string? Name { get; set; }
    public required int? OrganizerCareerId { get; set; }
    public required int? OrganizerOrganizationId { get; set; }
    public required int? ForeingCareerId { get; set; }
    public required ActivityScopes? Scope { get; set; }
    public required DateTime? StartDateMin { get; set; }
    public required DateTime? StartDateMax { get; set; }
    public required DateTime? EndDateMin { get; set; }
    public required DateTime? EndDateMax { get; set; }
    public required ActivityStatus? Status { get; set; }
}