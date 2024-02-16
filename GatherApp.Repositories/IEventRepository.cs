using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Retrieves events associated with the user specified by the provided user ID.
        /// </summary>
        /// <param name="Id">The unique identifier (ID) of the user for whom to retrieve events.</param>
        /// <returns>An <see cref="IQueryable{Event}"/> representing the events associated with the specified user.</returns>
        IQueryable<Event> GetMyEvents(string Id);

        /// <summary>
        /// Retrieves events in the calendar associated with the user specified by the provided user ID.
        /// </summary>
        /// <param name="userId">The unique identifier (ID) of the user for whom to retrieve events in the calendar.</param>
        /// <returns>An <see cref="IQueryable{Event}"/> representing the events in the calendar associated with the specified user.</returns>
        IQueryable<Event> GetEventsForUser(string userId);
    }
}
