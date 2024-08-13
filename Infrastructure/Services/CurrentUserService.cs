using System.Security.Claims;
using Application.Contracts;
using Application.Features.Users.Models;
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
    public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userRepository.GetByIdAsync(int.Parse(userId!), cancellationToken);
        
        return user!;
    }

    public async Task<Role> GetCurrentRoleAsync(CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userRepository.GetByIdAsync(int.Parse(userId!), cancellationToken);
        
        return user!.Role;
    }

    public async Task<UserResponse?> GetCurrentUserAsync()
    {
        return await Task.FromResult(new UserResponse());
    }
}