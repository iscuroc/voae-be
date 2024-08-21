using Domain.Enums;
using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;

public record JoinActivityCommand(
    int ActivityId,
    List<ActivityScopes> Scopes
) : ICommand<Result>;