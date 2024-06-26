using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        return user;
    }
}