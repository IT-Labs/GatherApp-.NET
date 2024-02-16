using Microsoft.AspNetCore.Authorization;

namespace GatherApp.API.Middleware
{
    public class CustomRoleAuthorizationRequirement: IAuthorizationRequirement
    {
        public string RoleName { get; }

        public CustomRoleAuthorizationRequirement(string roleName)
        {
            RoleName = roleName;
        }

    }
}
