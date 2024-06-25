using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Status : EntityBase
{
    public required string Name { get; set; }
}