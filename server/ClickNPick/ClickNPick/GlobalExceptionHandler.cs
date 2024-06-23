using ClickNPick.Application.Exceptions.General;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace ClickNPick.StartUp;

public class GlobalExceptionHandler : IExceptionHandler
{
    private const string ExceptionMessage = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Log.Error(exception, exception is Exception ? exception.Message : ExceptionMessage);
        var problemDetails = CreateProblemDetails(httpContext, exception);
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in Exception exception)
    {

        httpContext.Response.ContentType = "application/json";

        switch (exception)
        {
            case InvalidOperationException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case OperationFailedException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException _:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
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
