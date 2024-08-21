using Application.Contracts;
using Application.Features.Activities.Commands;
using Application.Shared;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.CommandHandlers;

public record UpdateActivityCommandHandler(
    IActivityRepository ActivityRepository,
    IUserMailer UserMailer,
    ICurrentUserService CurrentUserService,
    ICareerRepository CareerRepository,
    IUserRepository UserRepository,
    IOrganizationRepository OrganizationRepository
) : ICommandHandler<UpdateActivityCommand, Result>
{
    public async ValueTask<Result> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
        
        var activity = await ActivityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (activity is null) return Result.Failure(ActivityErrors.ActivityNotFound);
        
        if (activity.Slug != request.Name.Slugify())
        {
        var activitySlugExists = await ActivityRepository.ExistsBySlugAsync(request.Name.Slugify(), cancellationToken);
        if (activitySlugExists) return Result.Failure(ActivityErrors.ActivityNameAlreadyExists(request.Name));
        }
        
        if(currentUser != activity.RequestedBy) return Result.Failure(ActivityErrors.InvalidActivityOwner);
        
        if(activity.ActivityStatus != ActivityStatus.Rejected) return Result.Failure(ActivityErrors.InvalidActivityStatus);

        var supervisor = await UserRepository.GetByIdAsync(request.SupervisorId, cancellationToken);
        if (supervisor is null) return Result.Failure(ActivityErrors.SupervisorNotFound);
        if (supervisor.Role is not Role.Teacher) return Result.Failure(ActivityErrors.InvalidSupervisorRole);

        var coordinator = await UserRepository.GetByIdAsync(request.CoordinatorId, cancellationToken);
        if (coordinator is null) return Result.Failure(ActivityErrors.CoordinatorNotFound);
        if (coordinator.Role is not Role.Student) return Result.Failure(ActivityErrors.InvalidCoordinatorRole);

        List<Career> foreignCareers = [];
        foreach (var careerId in request.ForeignCareersIds)
        {
            var career = await CareerRepository.GetByIdAsync(careerId, cancellationToken);
            if (career is null) return Result.Failure(ActivityErrors.ForeignCareerNotFound(careerId));
            foreignCareers.Add(career);
        }

        List<ActivityOrganizer> organizers = [];
        foreach (var organizer in request.Organizers)
        {
            if (organizer is {Type: OrganizerType.Career, CareerId: not null})
            {
                var career = await CareerRepository.GetByIdAsync(organizer.CareerId.Value, cancellationToken);
                if (career is null) return Result.Failure(ActivityErrors.CareerOrganizerNotFound(organizer.CareerId.Value));
                organizers.Add(new ActivityOrganizer { Career = career });
                continue;
            }

            if (organizer is not {Type: OrganizerType.Organization, OrganizationId: not null}) continue;

            var organization = await OrganizationRepository.GetByIdAsync(organizer.OrganizationId.Value, cancellationToken);
            if (organization is null) return Result.Failure(ActivityErrors.OrganizationOrganizerNotFound(organizer.OrganizationId.Value));
            organizers.Add(new ActivityOrganizer { Organization = organization });
        }

        activity.Name = request.Name;
        activity.Slug = request.Name.Slugify();
        activity.Description = request.Description;
        activity.ForeingCareers = foreignCareers;
        activity.StartDate = request.StartDate;
        activity.EndDate = request.EndDate;
        activity.Goals = request.Goals.MapListToString();
        activity.Scopes = request.Scopes.Select(s => new ActivityScope { Scope = s.Scope, Hours = s.Hours}).ToList();
        activity.SupervisorId = request.SupervisorId;
        activity.CoordinatorId = request.CoordinatorId;
        activity.TotalSpots = request.TotalSpots;
        activity.Location = request.Location;
        activity.MainActivities = request.MainActivities.MapListToString();
        activity.Organizers = organizers;
        

        await ActivityRepository.UpdateAsync(activity, cancellationToken);

        return Result.Success();
    }
    }