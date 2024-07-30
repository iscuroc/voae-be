using System.Security.Claims;
using Application.Contracts;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    : ICurrentUserService
{
    public async Task<User> GetCurrentUser()
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var user = await userRepository.GetByIdAsync(int.Parse(userId));
        return user!;
    }

    public async Task<Role> GetCurrentUserRole()
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userRepository.GetByIdAsync(int.Parse(userId!));
        return user!.Role;
    }
}