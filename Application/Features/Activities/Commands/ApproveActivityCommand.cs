using Domain.Enums;
using Mediator;
using Shared;

namespace Application.Features.Activities.Commands;

public record ApproveActivityCommand(
    int ActivityId,
    string ReviewerObservation
) : ICommand<Result>;
