using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClickNPick.StartUp;

public class GlobalExceptionHandler : IExceptionHandler
{
    private const string ExceptionMessage = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //logger.LogError(exception, exception is Exception ? exception.Message : ExceptionMessage);
        var problemDetails = CreateProblemDetails(httpContext, exception);
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in Exception exception)
    {

        httpContext.Response.ContentType = "application/json";

        switch (exception)
        {
            case NotImplementedException notImplementedException:

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        return new ProblemDetails
        {
            Status = (int)httpContext.Response.StatusCode,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

    }
}
