using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Careers : EntityBase
{
    public required string Names { get; set; }
    public required string Lastname { get; set; }
    public required string Description { get; set; }
    public int FacultyId { get; set; }
    public int CoordinatorId { get; set; }
}