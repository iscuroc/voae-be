using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Career : EntityBase
{
    public required string Name { get; set; }
}