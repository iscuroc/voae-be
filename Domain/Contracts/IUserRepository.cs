using Domain.Entities;
using Domain.Enums;
using Shared;

namespace Domain.Contracts;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> AccoutNumberExistsAsync(long accountNumber, CancellationToken cancellationToken = default);
    Task<User?> GetByConfirmationTokenAsync(string confirmationToken, CancellationToken cancellationToken = default);
    Task<User?> GetByResetPasswordTokenAsync(string resetPasswordToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetByRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task<IEnumerable<Activity>> GetRequestsAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ActivityMember>> GetMyActivitiesAsync(int userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetPagedAsync(
        UserFilter filters,
        CancellationToken cancellationToken = default
    );

    Task<long> CountAsync(
        UserFilter filters,
        CancellationToken cancellationToken = default
    );
}

public record UserFilter : PaginationBase
{
    public string? Query { get; set; }
    public Role[]? Role { get; set; }
}