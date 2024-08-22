using Application.Contracts;
using Application.Features.Activities.Commands;
using Domain.Contracts;
using Domain.Enums;
using Domain.Entities;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.CommandHandlers
{
    public record RejectActivityCommandHandler(
        IActivityRepository ActivityRepository,
        IUserRepository UserRepository,
        IUserMailer UserMailer,
        ICurrentUserService CurrentUserService
    ) : ICommandHandler<RejectActivityCommand, Result>
    {
        public async ValueTask<Result> Handle(RejectActivityCommand request, CancellationToken cancellationToken)
        {

            var activity = await ActivityRepository.GetByIdAsync(request.ActivityId, cancellationToken);
            if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);

            var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
            if (currentUser.Role != Role.Voae) return Result.Failure(ActivityErrors.InvalidUserRole);

            if (activity.ActivityStatus != ActivityStatus.Pending)
                return Result.Failure(ActivityErrors.InvalidActivityStatusForRejection);

            activity.ActivityStatus = ActivityStatus.Rejected;
            activity.LastReviewedAt = DateTime.UtcNow;
            activity.ReviewerObservations = string.IsNullOrEmpty(activity.ReviewerObservations)
                ? request.ReviewerObservation
                : $"{activity.ReviewerObservations}|{request.ReviewerObservation}";
            activity.ReviewedById = currentUser.Id;

            var observationsList = activity.ReviewerObservations?
            .Split('|')
            .ToList()
            ?? new List<string>();


            await ActivityRepository.UpdateAsync(activity, cancellationToken);
            if (activity.RequestedBy != null)
            {
                await UserMailer.SendRejectActivityAsync(
                    activity.RequestedBy.Email,
                     observationsList,
                    cancellationToken
            );
            }

            return Result.Success();
        }
    }

}
