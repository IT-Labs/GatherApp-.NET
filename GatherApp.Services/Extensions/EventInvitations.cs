using GatherApp.Contracts.Entities;
using GatherApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.Services.Extensions
{
    public class EventInvitations
    {
        public static void GetEventInvitations(IUnitOfWork _unitOfWork, IEnumerable<Event> events)
        {
            var eventIds = events.Select(ev => ev.Id).ToList();

            var allInvitations = _unitOfWork.InvitationRepository
                .GetAll()
                .Include(x => x.User)
                .Where(inv => eventIds.Contains(inv.EventId))
                .ToList();

            events.ToList().ForEach(ev =>
            {
                ev.Invitations = allInvitations
                    .Where(inv => inv.EventId == ev.Id)
                    .ToList();
            });
        }
    }
}
