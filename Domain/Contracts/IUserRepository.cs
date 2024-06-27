using Domain.Entities;

namespace Domain.Contracts;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}