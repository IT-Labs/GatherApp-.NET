using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Entities;
using GatherApp.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GatherApp.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GatherAppContext gatherAppContext) : base(gatherAppContext)
        {
        }

        public User GetByEmail(string email)
        {
            return GetAll().AsQueryable().Include(x => x.Country).Include(x => x.Role).FirstOrDefault(e => e.Email.ToLower() == email.ToLower());
        }
        public User GetUserById(string userId)
        {
            return _gatherAppContext.Users.Where(x => x.Id == userId).Include(e => e.Country).Include(e => e.Role).FirstOrDefault();
        }

        public IQueryable<User> GetUsersSuggestionsByName(string name)
        {
            return GetAll()
                        .AsQueryable()
                        .Where(x => (x.FirstName.ToLower() + " " + x.LastName.ToLower())
                        .StartsWith(name.ToLower()) || (x.LastName.ToLower() + " " + x.FirstName.ToLower())
                        .StartsWith(name.ToLower())).OrderBy(user => user.FirstName + " " + user.LastName)
                        .Include(x => x.Country);
        }

        // used for Admin panel page
        public IQueryable<User> GetUsersSuggestionsByNameAndRole(string name, string role)
        {
            return GetUsersSuggestionsByName(name)
                        .Include(x => x.Role)
                        .Where(x => role == Values.RoleFilterAll || x.Role.RoleName == role);
        }

        // used for the Invite field on Create event page
        public IQueryable<User> GetThreeUsersSuggestionsByName(string name, List<string> countryIds)
        {
            if (!countryIds.Any())
                return GetUsersSuggestionsByName(name).Take(3);

            return GetUsersSuggestionsByName(name).Where(u => countryIds.Contains(u.CountryId)).Take(3);
        }
    }
}
