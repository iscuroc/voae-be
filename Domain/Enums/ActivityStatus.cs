namespace Domain.Enums;

public enum ActivityStatus
{
    RequestPending,
    RequestRejected,
    RequestApproved,
    ExecutionPending,
    ExecutionCancelled,
    ExecutionInProgress,
    ExecutionCompleted,
}
