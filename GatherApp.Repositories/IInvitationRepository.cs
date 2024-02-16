using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        /// <summary>
        /// Retrieves all event invitations associated with the specified event ID.
        /// </summary>
        /// <param name="eventId">The unique identifier (ID) of the event for which to retrieve invitations.</param>
        /// <returns>An <see cref="IQueryable{Invitation}"/> representing all event invitations for the specified event.</returns>
        public IQueryable<Invitation> GetAllEventInvites(string eventId);

        /// <summary>
        /// Removes all event invitations associated with the specified event ID.
        /// </summary>
        /// <param name="eventId">The unique identifier (ID) of the event for which to remove invitations.</param>
        public void RemoveAllWithEventId(string eventId);

        /// <summary>
        /// Removes event invitations based on the specified list of invitations.
        /// </summary>
        /// <param name="invitations">The list of invitations to be removed.</param>
        public void RemoveByList(List<Invitation> invitations);
    }
}
