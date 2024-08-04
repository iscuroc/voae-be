using Domain.Base;

namespace Domain.Entities;

public class Organization : EntityBase
{
    public required string Name { get; set; }
    
    public ICollection<ActivityOrganizer> Activities { get; set; } = null!;
}