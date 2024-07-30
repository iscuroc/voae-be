using Shared;

namespace Domain.Errors;

public static class ActivityErrors
{
    public static Error TeacherNotFound => Error.NotFound(
        "Activity.TeacherNotFound",
        "Teacher not found"
    );
    
    public static Error StudentNotFound => Error.NotFound(
        "Activity.StudentNotFound",
        "Student not found"
    );
    
    public static Error CareerNotFound => Error.NotFound(
        "Activity.CareerNotFound",
        "Career not found"
    );
    
    public static Error AvailableCareerNotFound(int id) => Error.NotFound(
        "Activity.AvailableCareerNotFound",
        $"Career with id {id} not found"
    );
    
    public static Error InvalidTeacherRole => Error.Conflict(
        "Activity.InvalidTeacherRole",
        "The user is not a teacher"
    );

    public static Error InvalidStudentRole => Error.Conflict(
        "Activity.InvalidStudentRole",
        "The user is not a student"
        );
    
}