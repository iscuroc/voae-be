using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class ActivityScope : EntityBase
{
    public ActivityScopes Scope { get; set; }

    public int HourAmount { get; set; }

    public int ActivityId { get; set; }

    public virtual Activity Activity { get; set; }
}