using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using GatherApp.DataContext;
using GatherApp.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.API.Filters
{
    public class OwnEventOrAdminActionFilter : IAsyncActionFilter
    {
        private readonly GatherAppContext _gatherAppContext;

        public OwnEventOrAdminActionFilter(GatherAppContext gatherAppContext)
        {
            _gatherAppContext = gatherAppContext ?? throw new ArgumentNullException(nameof(gatherAppContext));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tokenUserId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
            var tokenUserRole = JWTClaimsReader.HandleJWTClaims(Values.ClaimRoleName);

            var routeData = context.RouteData;
            if (routeData.Values.TryGetValue("Id", out var eventIdValue))
            {
                var eventId = eventIdValue?.ToString();
                var eventObject = _gatherAppContext.Events.FirstOrDefaultAsync(x => x.Id == eventId);

                if (eventObject.Result.DeletedAt != null)
                {
                    throw new ArgumentNullException(Errors.EventNotFound);
                }

                if (tokenUserRole.Equals(RoleEnum.Admin.ToString()))
                {
                    await next();
                    return;
                }
                else
                {
                    await EditOrDeleteEventAsUser(tokenUserRole, tokenUserId, eventObject, context, next);
                }
                
            }
        }

        private static async Task EditOrDeleteEventAsUser(string tokenUserRole, string tokenUserId, Task<Event> eventObject, ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (tokenUserRole.Equals(RoleEnum.User.ToString()) && tokenUserId != eventObject.Result.CreatedBy)
            {
                context.Result = new UnauthorizedObjectResult(Errors.Unauthorized);
                return;
            }

            await next();
            return;
        }
    }
}
