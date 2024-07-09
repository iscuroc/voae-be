using Domain.Base;

namespace Domain.Entities;

public class Career : EntityBase
{
    public required string Name { get; set; }
}