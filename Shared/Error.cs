
namespace Shared;

public sealed record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.InternalServerError);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided", ErrorType.InternalServerError);

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }
    
    public static Error BadRequest(string code, string description) => new(code, description, ErrorType.BadRequest);
    public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
    public static Error Unauthorized(string code, string description) => new(code, description, ErrorType.Unauthorized);
    public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
    public static Error InternalServerError(string code, string description) => new(code, description, ErrorType.InternalServerError);
}


public enum ErrorType
{
    BadRequest = 0,
    NotFound = 1,
    Unauthorized = 2,
    Conflict = 3,
    InternalServerError = 4
}