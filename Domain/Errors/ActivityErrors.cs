using Shared;

namespace Domain.Errors;

public static class ActivityErrors
{
    public static Error ActivityNameAlreadyExists(string name) => Error.Conflict(
        "Activity.ActivityNameAlreadyExists",
        $"An Activity with the name {name} already exists"
    );
    public static Error SupervisorNotFound => Error.NotFound(
        "Activity.SupervisorNotFound",
        "Supervisor was not found"
    );

    public static Error CoordinatorNotFound => Error.NotFound(
        "Activity.CoordinatorNotFound",
        "Coordinator was not found"
    );

    public static Error CareerOrganizerNotFound(int id) => Error.NotFound(
        "Activity.CareerOrganizerNotFound",
        $"The Career Organizer with id {id} was not found"
    );

    public static Error OrganizationOrganizerNotFound(int id) => Error.NotFound(
        "Activity.OrganizationOrganizerNotFound",
        $"The Organization Organizer with id {id} was not found"
    );

    public static Error ForeignCareerNotFound(int id) => Error.NotFound(
        "Activity.ForeignCareerNotFound",
        $"The foreign career with id {id} was not found"
    );

    public static Error InvalidSupervisorRole => Error.Conflict(
        "Activity.InvalidSupervisorRole",
        "The Supervisor must be a teacher"
    );

    public static Error InvalidCoordinatorRole => Error.Conflict(
        "Activity.InvalidCoordinatorRole",
        "The Coordinator must be a student"
    );

    public static Error ActivitySlugNotFound => Error.NotFound(
        "Activity.ActivitySlugNotFound",
        "Activity with the provided slug was not found"
    );
}