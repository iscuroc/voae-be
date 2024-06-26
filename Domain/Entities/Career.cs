using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Career : EntityBase
{
    //Cambios realizados: Carlos Romero
    public required string Name { get; set; }
    public int FacultyId { get; set; }
    public int CoordinatorId { get; set; }
}