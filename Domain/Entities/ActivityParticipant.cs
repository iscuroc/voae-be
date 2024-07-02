using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class ActivityParticipant : EntityBase
{
    //public int ActivityId { get; set; }
    //public int ParticipantId { get; set; }
    public ParticipationType ParticipantType { get; set; }
    public bool AttendanceConfirmation { get; set; }
}
