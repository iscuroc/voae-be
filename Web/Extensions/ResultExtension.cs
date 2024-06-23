using Shared;

namespace Web.Extensions;

public static class ResultExtension
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }
        
        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            type: GetType(result.Error.Type),
            extensions: new Dictionary<string, object?>
            {
                {
                    "errors", new[]
                    {
                        result.Error
                    }
                }
            }
        );
        
        static int GetStatusCode(ErrorType type) => type switch
        {
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        static string GetTitle(ErrorType type) => type switch
        {
            ErrorType.BadRequest => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Unauthorized => "Unauthorized",
            ErrorType.Conflict => "Conflict",
            ErrorType.InternalServerError => "Internal Server Error",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        static string GetType(ErrorType type) => type switch
        {
            ErrorType.BadRequest => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorType.InternalServerError => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}