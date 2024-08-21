using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user;
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .Include(u => u.Career)
            .Include(u => u.Organizations)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    { 
        return await context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> AccoutNumberExistsAsync(long accountNumber, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(u => u.AccountNumber == accountNumber, cancellationToken);
    }

    public async Task<User?> GetByConfirmationTokenAsync(string confirmationToken,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.EmailConfirmationToken == confirmationToken, cancellationToken);
        
        return user;
    }

    public async Task<User?> GetByResetPasswordTokenAsync(string resetPasswordToken, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.ResetPasswordToken == resetPasswordToken, cancellationToken);
        
        return user;
    }
    
    public async Task<IEnumerable<User>> GetByRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        return await context.Users.Where(u => u.Role == role).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Activity>> GetRequestsAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.Activities
            .Where(a => a.RequestedById == userId)
            .AddIncludes()
            .ToListAsync(cancellationToken);
    }
}