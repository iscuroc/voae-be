using Application.Contracts;
using Application.Features.Activities.Commands;
using Application.Shared;
using Domain.Contracts;
using Domain.Enums;
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

            var reviewerObservations = activity.ReviewerObservations!.MapStringToList();
            var user = await UserRepository.GetByIdAsync(activity.RequestedById, cancellationToken);
            await UserMailer.SendActivityRejectAsync(user!.Email, activity.Name, reviewerObservations, cancellationToken);

            await ActivityRepository.UpdateAsync(activity, cancellationToken);

            return Result.Success();
        }
    }
}
