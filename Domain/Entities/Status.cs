using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Status : EntityBase
{
    //Tabla implementada en TablesRoot

    public required string Name { get; set; }
}