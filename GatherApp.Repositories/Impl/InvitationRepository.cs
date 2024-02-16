using GatherApp.Contracts.Entities;
using GatherApp.DataContext;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.Repositories.Impl
{
    public class InvitationRepository : Repository<Invitation>, IInvitationRepository
    {
        public InvitationRepository(GatherAppContext dataContext) : base(dataContext)
        {
        }
        public IQueryable<Invitation> GetAllEventInvites(string eventId)
        {
            return GetAll().Include(x => x.User).Where(inv => inv.EventId == eventId);
        }

        public void RemoveAllWithEventId(string eventId)
        {
            _gatherAppContext.RemoveRange(GetAllEventInvites(eventId));
        }
        public void RemoveByList(List<Invitation> invitations)
        {
            _gatherAppContext.RemoveRange(invitations);
        }
    }
}
