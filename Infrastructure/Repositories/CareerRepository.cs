using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    public async Task<IEnumerable<User>> GetStudentsByIdAsync(int id, string query = "", CancellationToken cancellationToken = default)
    {
        if (query.IsNullOrEmpty()) 
        {
            return await context.Users
            .Where(s => s.CareerId == id)
            .Take(15)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        }

        var queryStudentsNames = context.Users
        .Where(s => s.CareerId == id && s.Names.Contains(query));

        var queryStudentsLastNames = context.Users
        .Where(s => s.CareerId == id && s.Lastnames.Contains(query));

        return await context.Users
            .Where(s => s.CareerId == id && s.Email.Contains(query))
            .Union(queryStudentsNames)
            .Union(queryStudentsLastNames)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}