using GatherApp.Contracts.Entities;
using GatherApp.DataContext;

namespace GatherApp.Repositories.Impl
{
    public class PasswordRepository : Repository<PasswordToken>, IPasswordRepository 
    {
        public PasswordRepository(GatherAppContext dataContext) : base(dataContext)
        {
        }

        public PasswordToken GetByToken(string token)
        {
            return _gatherAppContext.PasswordChange.FirstOrDefault(u => u.Token == token);
        }

    }
}
