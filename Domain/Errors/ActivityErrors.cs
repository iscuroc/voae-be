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

    public static Error ActivityNotFound => Error.NotFound(
        "Activity.ActivityNotFound",
        $"Activity with the provided id was not found"
    );

    public static Error InvalidActivityStatusForRejection => Error.Conflict(
        "Activity.InvalidActivityStatusForRejection",
        "Activity can only be rejected if its status is Pending"
    );

    public static Error InvalidUserRole => Error.Conflict(
        "Activity.InvalidActivityStatusForRejection",
        "Activity can only be rejected by VOAE users"
    );

    public static Error InvalidActivityStatusForApproval => Error.Conflict(
        "Activity.InvalidActivityStatusForApproval",
        "Activity can only be approved if its status is Pending or Rejected"
    );

    public static Error InvalidApprovalUserRole => Error.Conflict(
        "Activity.InvalidApprovalUserRole",
        "Activity can only be approved by VOAE users"
    );

    public static Error InvalidJoinUserRole => Error.Conflict(
        "Activity.InvalidJoinUserRole",
        "Activity can only be joined by a student"
    );

    public static Error InvalidActivityStatus => Error.Conflict(
        "Activity.InvalidActivityStatus",
        "Activity can only be joined if its status is Published"
    );

    public static Error AlreadyJoinedActivity => Error.Conflict(
        "Activity.AlreadyJoinedActivity",
        "User already joined the activity"
    );
    public static Error InvalidScope => Error.Conflict(
        "Activity.InvalidScope",
        "The scope is not valid for the activity"
    );
}