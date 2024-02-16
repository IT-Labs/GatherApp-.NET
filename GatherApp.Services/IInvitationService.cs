using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;

namespace GatherApp.Services
{
    public interface IInvitationService
    {
        /// <summary>
        /// Responds to an event invitation based on the specified request.
        /// </summary>
        /// <param name="request">The request containing information to update the invitation status (e.g. Going, Maybe, Not Going).</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="string"/> indicating the updated status.</returns>
        Response<string> RespondToEventInvitation(UpdateInviteStatusRequest request);
    }
}
