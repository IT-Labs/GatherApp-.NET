using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;

namespace GatherApp.Services
{
    public interface IEventService
    {
        /// <summary>
        /// Retrieves a single event based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the unique identifier (Id) of the event to retrieve.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="SingleEventResponse"/> providing information about the retrieved event.</returns>
        Response<SingleEventResponse> GetById(GetByIdRequest request);

        /// <summary>
        /// Creates a new event based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the necessary information to create the event.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="string"/> representing the unique identifier of the created event.</returns>
        Response<SingleEventResponse> CreateEvent(CreateEventRequest request);

        /// <summary>
        /// Soft Deletes an event based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the unique identifier (Id) of the event to soft delete.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the deletion was successful.</returns>
        Response<bool> DeleteById(GetByIdRequest request);

        /// <summary>
        /// Updates an existing event based on the specified event ID and request.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to update.</param>
        /// <param name="request">The request containing the updated information for the event.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the update was successful.</returns>
        Response<bool> UpdateEvent(string eventId, UpdateEventRequest request);

        /// <summary>
        /// Approves an event based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the necessary information to approve the event.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the approval was successful.</returns>
        Response<bool> ApproveEvent(ApproveEventStatusRequest request);

        /// <summary>
        /// Declines an event based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the necessary information to decline the event.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the decline was successful.</returns>
        Response<bool> DeclineEvent(DeclineEventStatusRequest request);

        /// <summary>
        /// Retrieves events based on the specified query request.
        /// </summary>
        /// <param name="request">The request containing the criteria for querying events.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with an <see cref="EventResponse"/> providing information about the retrieved events.</returns>
        Response<EventResponse> GetEvents(GetEventsRequest request);

        /// <summary>
        /// Retrieves event requests based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the criteria for retrieving event requests.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with an <see cref="EventResponse"/> providing information about the retrieved event requests.</returns>
        Response<EventResponse> GetEventRequests(GetEventRequestsRequest request);

        /// <summary>
        /// Retrieves all calendar events for the user.
        /// </summary>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with an <see cref="IEnumerable{UserCalendarEventResponse}"/> representing all events for the user.</returns>
        Response<IEnumerable<UserCalendarEventResponse>> GetAllEventsForUserCalendar();

        /// <summary>
        /// Retrieves event invitations for the user based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the user's unique identifier (Id).</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with an <see cref="IEnumerable{EventInvitationDto}"/> representing event invitations for the user.</returns>
        Response<IEnumerable<EventInvitationDto>> GetInvitations(GetByIdRequest request);
    }
}
