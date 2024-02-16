using GatherApp.Contracts.Constants;
using GatherApp.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GatherApp.API.Filters
{
    public class MyEventResponseActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tokenUserId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);

            var routeData = context.RouteData;
            if (routeData.Values.TryGetValue("Id", out var userIdValue))
            {
                var userId = userIdValue?.ToString();

                if (tokenUserId != userId)
                {
                    context.Result = new UnauthorizedObjectResult(Errors.Unauthorized);
                    return;
                }
            }

            await next();
        }
    }
}
