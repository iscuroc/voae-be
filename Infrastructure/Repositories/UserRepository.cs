using Domain.Contracts;
using Domain.Entities;
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
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
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
}