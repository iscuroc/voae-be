using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts;

public interface ICurrentUserService
{
    Task<User> GetCurrentUser(CancellationToken cancellationToken = default);
    Task<Role> GetCurrentUserRole(CancellationToken cancellationToken = default);
}