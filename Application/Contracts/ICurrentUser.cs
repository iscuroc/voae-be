using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts;

public interface ICurrentUserService
{
    Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default);
    Task<Role> GetCurrentRoleAsync(CancellationToken cancellationToken = default);
}