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
        IUserMailer UserMailer,
        ICurrentUserService CurrentUserService
    ) : ICommandHandler<ApproveActivityCommand, Result>
    {
        public async ValueTask<Result> Handle(ApproveActivityCommand request, CancellationToken cancellationToken)
        {

            var activity = await ActivityRepository.GetByIdAsync(request.ActivityId, cancellationToken);
            if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);

            var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
            if (currentUser.Role != Role.Voae) return Result.Failure(ActivityErrors.InvalidApprovalUserRole);

            activity.ActivityStatus = ActivityStatus.Approved;
            activity.LastReviewedAt = DateTime.UtcNow;
            
            var user = await UserRepository.GetByIdAsync(activity.RequestedById, cancellationToken);

            await UserMailer.SendActivityApprovedAsync(user!.Email, activity.Slug, cancellationToken);
            
            await ActivityRepository.UpdateAsync(activity, cancellationToken);

            return Result.Success();
        }
    }
}