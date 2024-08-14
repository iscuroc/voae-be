using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class ActivityScope : EntityBase
{
    public ActivityScopes Scope { get; set; }

    public required int Hours { get; set; }

    public int ActivityId { get; set; }

    public Activity Activity { get; set; } = null!;
}