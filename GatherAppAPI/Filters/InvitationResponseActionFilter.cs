using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;
using GatherApp.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GatherApp.API.Filters
{
    public class InvitationResponseActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tokenUserId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);

            if (context.ActionArguments.TryGetValue("request", out var requestObject))
            {
                if (requestObject is UpdateInviteStatusRequest updateInviteStatusRequest)
                {
                    var userIdFromRequest = updateInviteStatusRequest.UserId;

                    if (tokenUserId != userIdFromRequest)
                    {
                        context.Result = new UnauthorizedObjectResult(Errors.Unauthorized);
                        return;
                    }
                }
            }

            await next();
        }
    }
}
