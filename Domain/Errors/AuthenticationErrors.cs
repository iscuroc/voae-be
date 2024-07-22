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
    public static Error InvalidToken => Error.Unauthorized(
        "Authentication.InvalidToken",
        "Invalid token"
    );
    public static Error EmailAlreadyConfirmed => Error.Conflict(
        "Authentication.EmailAlreadyConfirmed",
        "Email already confirmed"
    );
    public static Error TokenExpired => Error.Unauthorized(
        "Authentication.TokenExpired",
        "Token expired"
    );

    public static Error InvalidAccountNumber => Error.Conflict(
        "Authentication.InvalidAccountNumber",
        "Invalid account number"
    );
    
    public static Error AccountNumberInUse => Error.Conflict(
        "Authentication.AccountNumberInUse",
        "Account number is already in use"
    );
}