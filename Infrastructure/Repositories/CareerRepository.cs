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
    
    public async Task<IEnumerable<User>> GetTeachersByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .Where(u => u.CareerId == id && u.Role == Role.Teacher && u.EmailConfirmedAt.HasValue)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetStudentsByIdAsync(int id, string query = "", CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query)) 
        {
            return await context.Users
            .Where(s => s.CareerId == id && s.EmailConfirmedAt.HasValue)
            .Take(15)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        }

        var queryStudentsNames = context.Users
        .Where(s => s.CareerId == id && s.Names.Contains(query) && s.EmailConfirmedAt.HasValue);

        var queryStudentsLastNames = context.Users
        .Where(s => s.CareerId == id && s.Lastnames.Contains(query) && s.EmailConfirmedAt.HasValue);

        return await context.Users
            .Where(s => s.CareerId == id && s.Email.Contains(query) && s.EmailConfirmedAt.HasValue)
            .Union(queryStudentsNames)
            .Union(queryStudentsLastNames)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}