using Application.Contracts;
using Application.Features.Activities.Commands;
using Application.Features.Activities.Mappers;
using Application.Shared;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;

namespace Application.Features.Activities.CommandHandlers;

public record CreateActivityCommandHandler(
    IActivityRepository ActivityRepository,
    IUserMailer UserMailer,
    ICurrentUserService CurrentUserService,
    ICareerRepository CareerRepository,
    IUserRepository UserRepository,
    IOrganizationRepository OrganizationRepository
) : ICommandHandler<CreateActivityCommand, Result>
{
    public async ValueTask<Result> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var activitySlugExists = await ActivityRepository.ExistsBySlugAsync(request.Name.Slugify(), cancellationToken);
        if (activitySlugExists) return Result.Failure(ActivityErrors.ActivityNameAlreadyExists(request.Name));
        
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
               
               organizers.Add(new ActivityOrganizer
               {
                   Career = career
               });
               
               continue;
            }

            if (organizer is not {Type: OrganizerType.Organization, OrganizationId: not null}) continue;
            
            var organization =
                await OrganizationRepository.GetByIdAsync(organizer.OrganizationId.Value, cancellationToken);
            if (organization is null)
                return Result.Failure(ActivityErrors.OrganizationOrganizerNotFound(organizer.OrganizationId.Value));

            organizers.Add(new ActivityOrganizer
            {
                Organization = organization
            });
        }
        
        var currentUser = await CurrentUserService.GetCurrentUserAsync(cancellationToken);
        
        var status = currentUser.Role switch
        {
            Role.Voae => ActivityStatus.Approved,
            _ => ActivityStatus.Pending
        };
        
        var activity = request.ToEntity(status, foreignCareers, organizers, requestedById: currentUser.Id);

        await ActivityRepository.AddAsync(activity, cancellationToken);
        
        var voaeUsers = await UserRepository.GetByRoleAsync(Role.Voae, cancellationToken);

        var tasks = voaeUsers.Select(user => 
            UserMailer.SendActivityRequestedAsync(
            user.Email, 
            activity.Slug, 
            cancellationToken
        )).ToList();
        
        await Task.WhenAll(tasks);

        return Result.Success();
    }
}