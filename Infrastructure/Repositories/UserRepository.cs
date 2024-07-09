using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    { 
        return await context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}