using Domain.Base;

namespace Domain.Entities;

public class ActivityOrganizer : EntityBase
{
    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = null!;
    
    public int? CareerId { get; set; }
    public Career? Career { get; set; } = null!;
    
    public int? OrganizationId { get; set; }
    public Organization? Organization { get; set; } = null!;
}