using Application.Contracts;
using Application.Features.Activities.Commands;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;
using ActivityMemberScope = Domain.Entities.ActivityMemberScope;

namespace Application.Features.Activities.CommandHandlers;

public record JoinActivityCommandHandler(
    IActivityRepository ActivityRepository,
    ICurrentUserService CurrentUserService
) : ICommandHandler<JoinActivityCommand, Result>
{
    public async ValueTask<Result> Handle(JoinActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await ActivityRepository.GetByIdAsync(request.ActivityId, cancellationToken);
        if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);

        if (activity.ActivityStatus != ActivityStatus.Published)
            return Result.Failure(ActivityErrors.InvalidJoinedStatus);

        var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
        if (currentUser.Role != Role.Student)
            return Result.Failure(ActivityErrors.InvalidJoinUserRole);

        if (activity.Members.Any(member => member.MemberId == currentUser.Id))
            return Result.Failure(ActivityErrors.AlreadyJoinedActivity);

        if (request.Scopes.Any(requestScope => activity.Scopes.All(scope => scope.Scope != requestScope)))
            return Result.Failure(ActivityErrors.InvalidScope);

        var activityMemberScopes = request.Scopes.Select(requestScope => new ActivityMemberScope
        {
            MemberScope = requestScope,
            Hours = activity.Scopes.First(s => s.Scope == requestScope).Hours
        }).ToList();

        var activityMember = new ActivityMember
        {
            ActivityId = activity.Id,
            MemberId = currentUser.Id,
            Scopes = activityMemberScopes
        };

        await ActivityRepository.AddMemberAsync(activityMember, cancellationToken);

        return Result.Success();
    }
}