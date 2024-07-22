using Domain.Entities;

namespace Domain.Contracts;

public interface IUserRepository
{
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    
}
