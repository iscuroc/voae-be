using Application.Contracts;
using Application.Features.Activities.Commands;
using Application.Shared;
using Domain.Contracts;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.CommandHandlers;

public record ActivityBannerCommandHandler(
    IActivityRepository ActivityRepository,
    IStorageService StorageService,
    ICurrentUserService CurrentUserService
) : ICommandHandler<ActivityBannerCommand, Result>
{
    public async ValueTask<Result> Handle(ActivityBannerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);

        var activity = await ActivityRepository.GetByIdAsync(request.ActivityId, cancellationToken);
        if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);

        if (currentUser != activity.RequestedBy)
            return Result.Failure(ActivityErrors.InvalidActivityOwner);

        if (activity.ActivityStatus is ActivityStatus.Completed or ActivityStatus.Cancelled)
            return Result.Failure(ActivityErrors.CannotUploadBanner);

        if (request.Banner.Length > 3000000) return Result.Failure(ActivityErrors.InvalidBannerSize);
        if (request.Banner.ContentType is not "image/jpeg" and not "image/png")
            return Result.Failure(ActivityErrors.InvalidBannerType);
        
        var folderName = $"activities/{activity.Id}";
        var bannerUrl = await StorageService.UploadAsync(request.Banner, folderName);
        
        if(string.IsNullOrWhiteSpace(bannerUrl)) 
            return Result.Failure(ActivityErrors.BannerUploadFailed);
        
        activity.BannerLink = bannerUrl;
        await ActivityRepository.UpdateAsync(activity, cancellationToken);

        return Result.Success();
    }
}