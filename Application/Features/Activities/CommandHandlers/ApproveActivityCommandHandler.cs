using Application.Contracts;
using Application.Features.Activities.Commands;
using Domain.Contracts;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.CommandHandlers
{
    public record ApproveActivityCommandHandler(
        IActivityRepository ActivityRepository,
        IUserRepository UserRepository,
        ICurrentUserService CurrentUserService
    ) : ICommandHandler<ApproveActivityCommand, Result>
    {
        public async ValueTask<Result> Handle(ApproveActivityCommand request, CancellationToken cancellationToken)
        {

            var activity = await ActivityRepository.GetByIdAsync(request.ActivityId, cancellationToken);
            if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);

            var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
            if (currentUser.Role != Role.Voae) return Result.Failure(ActivityErrors.InvalidApprovalUserRole);

            if (!(activity.ActivityStatus == ActivityStatus.Pending || activity.ActivityStatus == ActivityStatus.Rejected))
                return Result.Failure(ActivityErrors.InvalidActivityStatusForApproval);

            activity.ActivityStatus = ActivityStatus.Approved;
            activity.LastReviewedAt = DateTime.UtcNow;
            activity.ReviewerObservations = string.IsNullOrEmpty(activity.ReviewerObservations)
                ? request.ReviewerObservation
                : $"{activity.ReviewerObservations}|{request.ReviewerObservation}";
            activity.ReviewedById = currentUser.Id;

            await ActivityRepository.UpdateAsync(activity, cancellationToken);
            var Users = await UserRepository.GetByRoleAsync(Role.Student, cancellationToken);

            var tasks = Users.Select(user =>
                UserMailer.SendApproveActivityAsync(
                user.Email,
                activity.Slug,
                cancellationToken
            )).ToList();

            await Task.WhenAll(tasks);

            return Result.Success();
        }
    }
}