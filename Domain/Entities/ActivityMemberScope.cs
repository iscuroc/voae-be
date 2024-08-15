using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class ActivityMemberScope : EntityBase
{
    public required ActivityScopes MemberScope { get; set; }

    public required int Hours { get; set; }
    
    public int ActivityMemberId { get; set; }
    public ActivityMember ActivityMember { get; set; } = null!;
}