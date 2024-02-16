using GatherApp.Contracts.Entities;
using GatherApp.DataContext;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.Repositories.Impl
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(GatherAppContext dataContext) : base(dataContext)
        {
        }

        public Token GetByValue(string value)
        {
            return _gatherAppContext.Tokens.Include(t => t.User.Role).FirstOrDefault(t => t.Value == value);
        }
    }
}
