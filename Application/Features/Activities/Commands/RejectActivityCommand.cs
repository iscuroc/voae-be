using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;
public record RejectActivityCommand(
    int Id,
    string ReviewerObservation
) : ICommand<Result>;

