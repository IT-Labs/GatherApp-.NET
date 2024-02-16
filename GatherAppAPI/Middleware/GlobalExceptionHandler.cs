using GatherApp.Services;
using GatherApp.Services.Impl;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILoggingService _loggingService;

    public GlobalExceptionHandler(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _loggingService.LogError("Unhandled exception", exception);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unhandled exception occurred"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}