using Application.Contracts;
using Application.Features.Activities.Commands;
using Domain.Contracts;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.CommandHandlers
{
    public record PublishActivityCommandHandler(
        IActivityRepository ActivityRepository,
        IUserRepository UserRepository,
        IUserMailer UserMailer
    ) : ICommandHandler<PublishActivityCommand, Result>
    {
        public async ValueTask<Result> Handle(PublishActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await ActivityRepository.GetByIdAsync(request.ActivityId, cancellationToken);
            if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);

            if (string.IsNullOrWhiteSpace(activity.BannerLink))
                return Result.Failure(ActivityErrors.ActivityBannerRequired);
            if(activity.ActivityStatus != ActivityStatus.Approved)
                return Result.Failure(ActivityErrors.InvalidActivityStatusForPublishing);

            activity.ActivityStatus = ActivityStatus.Published;

            var user = await UserRepository.GetByIdAsync(activity.RequestedById, cancellationToken);

            await UserMailer.SendActivityPublishedAsync(user!.Email, activity.Name, cancellationToken);

            await ActivityRepository.UpdateAsync(activity, cancellationToken);

            return Result.Success();
        }
    }
}