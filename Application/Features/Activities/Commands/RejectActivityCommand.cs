using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;
public record RejectActivityCommand(
    string Id,
    string ReviewerObservation
) : ICommand<Result>;

