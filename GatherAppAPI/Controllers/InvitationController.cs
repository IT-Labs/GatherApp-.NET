using GatherApp.API.Filters;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatherApp.API.Controllers
{
    [ApiController]
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        // PUT methods
        [HttpPut]
        [ServiceFilter(typeof(InvitationResponseActionFilter))]
        [Route("invitation/events/{eventId}/respond")]
        public Response<string> RespondToEventInvitation([FromRoute] string eventId, [FromBody] UpdateInviteStatusRequest request)
        {
            request.EventId = eventId;
            return _invitationService.RespondToEventInvitation(request);
        }
    }
}
