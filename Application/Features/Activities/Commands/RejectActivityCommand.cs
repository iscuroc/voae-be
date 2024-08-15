using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;
public record RejectActivityCommand(
    int ActivityId,
    string ReviewerObservation
) : ICommand<Result>;

