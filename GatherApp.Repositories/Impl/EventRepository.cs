using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using GatherApp.DataContext;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.Repositories.Impl
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(GatherAppContext dataContext) : base(dataContext) 
        {
        }
        public IQueryable<Event> GetMyEvents(string id)
        {
            return GetAll().Where(ev => ev.CreatedBy == id);
        }        
        public IQueryable<Event> GetEventsForUser(string userId)
        {
            // invite-only events to which a user has been invited
            var eventsInvited = _gatherAppContext.Invitations
                .Where(e => e.UserId == userId && (e.InviteStatus == InviteStatusEnum.Going || e.InviteStatus == InviteStatusEnum.Maybe || e.InviteStatus == InviteStatusEnum.NoResponse))
                .Select(e => e.Event)
                .Where(e => e.IsInviteOnly == true && (e.Status == EventStatusEnum.Approved.ToString() || e.Status == EventStatusEnum.Pending.ToString()));

            // get all events where user is going
            var eventsGoing = _gatherAppContext.Invitations.Where(i => i.UserId == userId && i.InviteStatus == InviteStatusEnum.Going).Select(i => i.Event);

            // get all events which are approved and pending
            var eventsApprovedOrPending = GetAll().Where(e => e.CreatedBy == userId && e.Status != EventStatusEnum.Declined.ToString());

            var query = eventsGoing.Union(eventsApprovedOrPending).Union(eventsInvited);
            return query;
        }
    }
}
