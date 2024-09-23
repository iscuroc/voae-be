using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;

public record PublishActivityCommand(
    int ActivityId
) : ICommand<Result>;