using Shared;

namespace Domain.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials => Error.Unauthorized(
        "Authentication.InvalidCredentials",
        "Invalid credentials"
    );
    
}