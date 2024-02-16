using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Retrieves a role based on the specified role name.
        /// </summary>
        /// <param name="roleName">The name of the role to retrieve.</param>
        /// <returns>A <see cref="Role"/> representing the role with the specified name.</returns>
        public Role GetRole(string roleName);
    }
}
