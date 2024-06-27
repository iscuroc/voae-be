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
    //public required Career CareerId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required int TotalRooms { get; set; }
    public int? OccupiedRooms { get; set; } = 0;
    public string? BannerLink { get; set; }
    //public required User SupervisorId { get; set; }
    //public required User RequestedById { get; set; }
    public required DateTime RequestDate { get; set; }
    public required RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    //public User? ReviewedBy { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? ReviewObservations { get; set; }
    public DevelopmentStatus? DevelopmentStatus { get; set; }

}