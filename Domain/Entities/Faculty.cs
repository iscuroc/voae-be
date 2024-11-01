﻿using Domain.Base;

namespace Domain.Entities;

public class Faculty : EntityBase
{
    public required string Name { get; set; }
    public IEnumerable<Career> Careers { get; set; } = null!;
}