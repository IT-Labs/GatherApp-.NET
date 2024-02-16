using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Retrieves a user based on the specified email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>A <see cref="User"/> representing the user with the specified email address.</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Retrieves a user based on the specified user ID.
        /// </summary>
        /// <param name="userId">The unique identifier (ID) of the user to retrieve.</param>
        /// <returns>A <see cref="User"/> representing the user with the specified user ID.</returns>
        User GetUserById(string userId);

        /// <summary>
        /// Retrieves users based on name suggestions.
        /// </summary>
        /// <param name="name">The name for which to retrieve user suggestions.</param>
        /// <returns>An <see cref="IQueryable{User}"/> representing user suggestions based on the specified name.</returns>
        IQueryable<User> GetUsersSuggestionsByName(string name);

        /// <summary>
        /// Retrieves users based on name and role suggestions.
        /// </summary>
        /// <param name="name">The name for which to retrieve user suggestions.</param>
        /// <param name="role">The role for which to retrieve user suggestions.</param>
        /// <returns>An <see cref="IQueryable{User}"/> representing user suggestions based on the specified name and role.</returns>
        IQueryable<User> GetUsersSuggestionsByNameAndRole(string name, string role);

        /// <summary>
        /// Retrieves three users based on name and country suggestions.
        /// </summary>
        /// <param name="name">The name for which to retrieve user suggestions.</param>
        /// <param name="countryIds">The list of countries for which to retrieve user suggestions.</param>
        /// <returns>An <see cref="IQueryable{User}"/> representing three user suggestions based on the specified name and countries.</returns>
        IQueryable<User> GetThreeUsersSuggestionsByName(string name, List<string> countryIds);
    }
}
