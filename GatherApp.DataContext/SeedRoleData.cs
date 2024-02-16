using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GatherApp.DataContext
{
    public static class SeedRoleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new GatherAppContext(serviceProvider.GetRequiredService<DbContextOptions<GatherAppContext>>());

            if (context.Roles.Any())
                return;

            IList<Role> roles = new List<Role>()
            {
                new Role()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleName = RoleEnum.Admin.ToString()
                },
                new Role()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleName = RoleEnum.User.ToString()
                }
            };
            context.Roles.AddRange(roles);

            context.SaveChanges();
        }
    }
}
