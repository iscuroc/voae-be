using Domain.Base;

namespace Domain.Entities;

public class Career : EntityBase
{
    public required string Name { get; set; }
    public required int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
    public ICollection<Activity> ForeingActivities { get; set; } = null!;
    public ICollection<ActivityOrganizer> Activities { get; set; } = null!;
}