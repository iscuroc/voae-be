using Domain.Entities;

namespace Domain.Contracts;

public interface ICareerRepository
{
    Task<IEnumerable<Career>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Career?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetTeachersByIdAsync(int id, CancellationToken cancellationToken = default);
    
}