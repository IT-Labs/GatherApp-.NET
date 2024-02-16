using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using GatherApp.DataContext;

namespace GatherApp.Repositories.Impl
{
    public class EmailRepository : Repository<Email>, IEmailRepository
    {

        public EmailRepository(GatherAppContext gatherAppContext) : base(gatherAppContext)
        {
        }

        public Email GetEmailTemplate(EmailEnum emailEnum)
        {
            return _gatherAppContext.EmailConstants.FirstOrDefault(x => x.Type == emailEnum.ToString());
        }
    }
}
