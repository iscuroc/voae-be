using Shared;

namespace Domain.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials => Error.Unauthorized(
        "Authentication.InvalidCredentials",
        "Invalid credentials"
    );
    public static Error EmailInUse => Error.Conflict(
        "Authentication.EmailInUse",
        "Email is already in use"
    );
}