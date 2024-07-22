using Domain.Base;

namespace Domain.Entities;

public class Career : EntityBase
{
    public required string Name { get; set; }
    
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    public IEnumerable<User> Users { get; set; } = null!;
}