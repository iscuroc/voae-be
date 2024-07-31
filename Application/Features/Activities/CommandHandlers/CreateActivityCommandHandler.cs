using Application.Contracts;
using Application.Features.Activities.Commands;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Mediator;
using Shared;
using ActivityScope = Domain.Entities.ActivityScope;

namespace Application.Features.Activities.CommandHandlers;

public record CreateActivityCommandHandler(
    IActivityRepository ActivityRepository,
    IUserNotificationService UserNotificationService,
    ICurrentUserService CurrentUserService,
    ICareerRepository CareerRepository,
    IUserRepository UserRepository
) : ICommandHandler<CreateActivityCommand, Result>
{
    public async ValueTask<Result> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        //TODO: si es voae poner la actividad en estado probada.
        var teacher = await UserRepository.GetByIdAsync(request.CareerTeacherId, cancellationToken);
        if (teacher is null) return Result.Failure(ActivityErrors.TeacherNotFound);
        if (teacher.Role is not Role.Teacher) return Result.Failure(ActivityErrors.InvalidTeacherRole);

        var student = await UserRepository.GetByIdAsync(request.CareerStudentId, cancellationToken);
        if (student is null) return Result.Failure(ActivityErrors.StudentNotFound);
        if (student.Role is not Role.Student) return Result.Failure(ActivityErrors.InvalidStudentRole);

        var careerExist = await CareerRepository.ExistsAsync(request.MainCareerId, cancellationToken);
        if (!careerExist) return Result.Failure(ActivityErrors.CareerNotFound);

        List<Career> availableCareers = [];

        foreach (var careerId in request.AvailableCareers)
        {
            var career = await CareerRepository.GetByIdAsync(careerId, cancellationToken);
            if (career is null) return Result.Failure(ActivityErrors.AvailableCareerNotFound(careerId));
            availableCareers.Add(career);   
        }

        //create scopes
        var scopes = request.Scopes.Select(scope => new ActivityScope
        {
            Scope = scope.Scope,
            HourAmount = scope.Hours
        }).ToList();

        ActivityStatus status =
            await CurrentUserService.GetCurrentUserRole() == Role.Voae
                ? ActivityStatus.RequestApproved
                : ActivityStatus.RequestPending;
        var activity = new Activity
        {
            Name = request.Name,
            Description = request.Description,
            Objectives = request.Goals,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalSpots = request.TotalSpots,
            MainCareerId = request.MainCareerId,
            TeacherId = request.CareerTeacherId,
            StudentId = request.CareerStudentId,
            Location = request.Location,
            ActivityStatus = status,
            MainActivities = string.Join("|", request.MainActivities),
            Scopes = scopes,
            ForeingCareers = availableCareers
        };

        await ActivityRepository.AddAsync(activity, cancellationToken);
        
        var voaeUsers = await UserRepository.GetUsersByRoleAsync(Role.Voae, cancellationToken);
        var activityLink = activity.Id;

        foreach (var user in voaeUsers)
        {
            await UserNotificationService.SendNewActivityEmailAsync(user.Email, activityLink, cancellationToken);
        }

        return Result.Success();
    }
}