using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class ActivityParticipant : EntityBase
{
    //public int ActivityId { get; set; }
    //public int ParticipantId { get; set; }
    public ParticipationType TipoParticipante { get; set; }
    public bool ConfirmarAsistencia { get; set; }
}
