using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CareerRepository(ApplicationDbContext context) : ICareerRepository
{
    public async Task<IEnumerable<Career>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Careers
            .Include(c => c.Faculty)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Career?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Careers.FindAsync([id, cancellationToken], cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Careers.AnyAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<User>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .Where(u => u.CareerId == id && u.Role == Role.Teacher)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    } 
    
}