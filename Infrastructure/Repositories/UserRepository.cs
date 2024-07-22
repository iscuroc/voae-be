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
    
    
    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await context.Users
            .Where(u => u.Email == user.Email) 
            .FirstOrDefaultAsync(cancellationToken);

        if (existingUser == null)
        {
            throw new InvalidOperationException("User not found");
        }

        
        existingUser.PasswordResetToken = user.PasswordResetToken;
        existingUser.PasswordResetTokenExpiresAt = user.PasswordResetTokenExpiresAt;
        

        await context.SaveChangesAsync(cancellationToken);
    }
}