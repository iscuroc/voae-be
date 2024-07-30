using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts;

public interface ICurrentUserService
{
    Task<User> GetCurrentUser();
    Task<Role> GetCurrentUserRole();
}