using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Enums;
using GatherApp.Services.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GatherApp.API.Middleware
{
    public class CustomRoleAuthorizationHandler : AuthorizationHandler<CustomRoleAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleAuthorizationRequirement requirement)
        {
            var principal = Thread.CurrentPrincipal;

            if (principal != null && principal.Identity !=null && principal.Identity.IsAuthenticated) 
            {
                var claims = context.User.Claims;
                var roleClaim = claims.FirstOrDefault(c => c.Type == Values.ClaimRoleName);
                if (roleClaim != null)
                {
                    var role = roleClaim.Value;

                    var currentRoleLevel = ParseEnumStringToInt.ToEnum<RoleEnum>(role);
                    var requirementLevel = ParseEnumStringToInt.ToEnum<RoleEnum>(requirement.RoleName);


                    // we are comparing the integer values of the User role and Requirement role;
                    // a role with higher priorities will be a higher number in our enum;
                    // example: a SuperAdmin (value of 3) should be able to do everything an Admin (value of 2) can, but not vice versa
                    if (currentRoleLevel >= requirementLevel)
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
