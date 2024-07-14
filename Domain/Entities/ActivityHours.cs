using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;


public class ActivityHour: EntityBase
{
    public ActivityScope ActivityScope {get; set; }
    
    public ParticipationType ParticipationType { get; set; }
    
    public int HourAmount { get; set; }
}