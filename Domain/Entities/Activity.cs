using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Activity : EntityBase
{
    public required string Slug { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required string MainActivities { get; set; }
    public required string Goals { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required int TotalSpots { get; set; }
    public string? BannerLink { get; set; }
    public required DateTime LastRequestedAt { get; set; }
    public ActivityStatus ActivityStatus { get; set; } = ActivityStatus.Pending;
    public DateTime? LastReviewedAt { get; set; }
    public string? ReviewerObservations { get; set; }
    
    public required int SupervisorId { get; set; }
    public User Supervisor { get; set; } = null!;
    
    public required int CoordinatorId { get; set; }
    public User Coordinator { get; set; } = null!;
    
    public required int RequestedById { get; set; }
    public User RequestedBy { get; set; } = null!;
    
    public int? ReviewedById { get; set; } 
    public User? ReviewedBy { get; set; }
    
    public required ICollection<ActivityScope> Scopes { get; set; } 
    public required ICollection<Career> ForeingCareers { get; set; }
    public required ICollection<ActivityOrganizer> Organizers { get; set; }
}