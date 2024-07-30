using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Activity : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required string MainActivities { get; set; }
    public required string Objectives { get; set; }
    public required int MainCareerId { get; set; }
    public  virtual Career MainCareer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalSpots { get; set; }
    public string? BannerLink { get; set; }
    public virtual ICollection<ActivityScope> Scopes { get; set; }
    public virtual ICollection<Career> ForeingCareers { get; set; }
    public required int TeacherId { get; set; }
    public virtual User Teacher { get; set; }
    public required int StudentId { get; set; }

    public virtual User Student { get; set; }

    //public required User RequestedById { get; set; }
    public DateTime RequestedAt { get; set; }

    public ActivityStatus ActivityStatus { get; set; } = ActivityStatus.RequestPending;

    //public User? ReviewedBy { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? ReviewObservations { get; set; }
}