using System.Security.Claims;
using Application.Contracts;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CurrentUserService(
    IHttpContextAccessor httpContextAccessor, 
    IUserRepository userRepository
) : ICurrentUserService
{
    public async Task<User> GetCurrentUser(CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userRepository.GetByIdAsync(int.Parse(userId!), cancellationToken);
        
        return user!;
    }

    public async Task<Role> GetCurrentUserRole(CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userRepository.GetByIdAsync(int.Parse(userId!), cancellationToken);
        
        return user!.Role;
    }
}