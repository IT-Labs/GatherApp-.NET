using GatherApp.Contracts.Entities;
using GatherApp.DataContext;

namespace GatherApp.Repositories.Impl
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {

        public RoleRepository(GatherAppContext dataContext) : base(dataContext)
        {
        }
        public Role GetRole (string roleName)
        {
            return _gatherAppContext.Roles.FirstOrDefault(u => u.RoleName == roleName);
        }
    }
}
