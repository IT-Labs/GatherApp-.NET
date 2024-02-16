using GatherApp.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace GatherApp.API.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILoggingService _loggingService;

        public LogActionFilter(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["ActionStartTime"] = Stopwatch.StartNew();

            var message = Log(context.RouteData, context.HttpContext, "Started");

            _loggingService.LogInformation(message);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var stopwatch = (Stopwatch)context.HttpContext.Items["ActionStartTime"];
            stopwatch.Stop();

            var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

            var message = Log(context.RouteData, context.HttpContext, $"Completed in {elapsedSeconds} seconds");

            _loggingService.LogInformation(message);
        }

        private string Log(RouteData routeData, HttpContext httpContext, string status)
        {
            var userId = httpContext.User.Identity?.Name ?? "Anonymous";
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var endpoint = $"{controllerName}/{actionName}";

            var request = httpContext.Request;

            var requestDetails = $"\nRequest Details: " +
                                $"\nMethod: {request.Method}" +
                                $"\nPath: {request.Path}" +
                                $"\nQueryString: {request.QueryString}" +
                                $"\nController: {controllerName}" +
                                $"\nAction: {actionName}" +
                                $"\nEndpoint: {endpoint}" +
                                $"\nStatus: {status}";

            return $"{requestDetails}";
        }

    }
}
