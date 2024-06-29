using Domain.Enums;

namespace Domain.Entities;


public class ActivitiesHours
{
    public ActivityScope ActivityScope {get; set; }
    
    public ParticipationType ParticipationType { get; set; }
    
    public int HourAmount { get; set; }
}