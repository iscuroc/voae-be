using Domain.Entities;

namespace Domain.Contracts;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> AccoutNumberExistsAsync(long accountNumber, CancellationToken cancellationToken = default);
    Task<User?> GetByConfirmationTokenAsync(string confirmationToken, CancellationToken cancellationToken = default);
}