using GatherApp.API.Filters;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatherApp.API.Controllers
{
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET methods
        [HttpGet("events")]
        public Response<EventResponse> GetAllEvents([FromQuery] GetEventsRequest request)
        {
            return _eventService.GetEvents(request);
        }

        [HttpGet("events/requests")]
        [Authorize(Policy = nameof(AuthorizationPolicyEnum.Admin))]
        public Response<EventResponse> GetEventRequests([FromQuery] GetEventRequestsRequest request)
        {
            return _eventService.GetEventRequests(request);
        }

        [HttpGet("events/{Id}")]
        public Response<SingleEventResponse> GetEvent([FromRoute] GetByIdRequest request)
        {
            return _eventService.GetById(request);
        }

        [HttpGet("events/{Id}/invitations")]
        public Response<IEnumerable<EventInvitationDto>> GetInvitations([FromRoute] GetByIdRequest request)
        {
            return _eventService.GetInvitations(request);
        }

        // for calendar in user profile
        [HttpGet("myEvents")]
        public Response<IEnumerable<UserCalendarEventResponse>> GetAllEventsForUser()
        {
            return _eventService.GetAllEventsForUserCalendar();
        }

        // POST methods
        [HttpPost("events")]
        public Response<SingleEventResponse> AddEvent([FromForm] CreateEventRequest entity)
        {
            return _eventService.CreateEvent(entity);
        }

        // PUT methods
        [HttpPut]
        [Route("events/{Id}")]
        [ServiceFilter(typeof(OwnEventOrAdminActionFilter))]
        public Response<bool> UpdateEvent([FromRoute] string Id, [FromForm] UpdateEventRequest request)
        {
            return _eventService.UpdateEvent(Id, request);
        }

        [HttpPut("events/{Id}/approve-event")]
        [Authorize(Policy = nameof(AuthorizationPolicyEnum.Admin))]
        public Response<bool> ApproveEvent([FromRoute] string Id, [FromBody] ApproveEventStatusRequest request)
        {
            request.Id = Id;
            return _eventService.ApproveEvent(request);
        }

        [HttpPut("events/{Id}/decline-event")]
        [Authorize(Policy = nameof(AuthorizationPolicyEnum.Admin))]
        public Response<bool> DeclineEvent([FromRoute] string Id, [FromBody] DeclineEventStatusRequest request)
        {
            request.Id = Id;
            return _eventService.DeclineEvent(request);
        }

        // DELETE methods
        [HttpDelete("events/{Id}")]
        [ServiceFilter(typeof(OwnEventOrAdminActionFilter))]
        public Response<bool> DeleteEvent([FromRoute] GetByIdRequest request)
        {
            return _eventService.DeleteById(request);
        }
    }
}
