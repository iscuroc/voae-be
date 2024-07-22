using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails();
        
        if (exception is CommandValidationException validationException)
        {
            problemDetails.Title = "One or more validation errors occurred.";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            problemDetails.Extensions.Add("errors", validationException.Errors);
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        problemDetails.Title = "Internal Server Error";
        problemDetails.Status = StatusCodes.Status500InternalServerError;
        problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}