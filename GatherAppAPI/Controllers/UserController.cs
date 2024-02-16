using GatherApp.API.Filters;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GatherApp.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET methods
        [HttpGet("users")]
        [Authorize(Policy = nameof(AuthorizationPolicyEnum.Admin))]
        public Response<UserResponse> GetListOfUsers([FromQuery] ListOfUsersRequest request)
        {
            return _userService.GetListOfUsers(request);
        }

        [HttpGet("users/{Id}")]
        public Response<SingleUserResponse> GetUser([FromRoute] GetByIdRequest request)
        {
            return _userService.GetById(request);
        }

        // fetch events created by a user with specified ID
        [HttpGet("users/{Id}/events")]
        [ServiceFilter(typeof(MyEventResponseActionFilter))]
        public Response<EventResponse> GetMyEvents([FromQuery] GetMyEventsRequest request, [FromRoute] GetByIdRequest userId )
        {
            MyEventsServiceRequest requestService = new MyEventsServiceRequest()
            {
                Organizer = request.Organizer,
                EventStatus = request.EventStatus,
                Page = request.Page,
                PageSize = request.PageSize,
                Id = userId.Id
            };
            return _userService.GetMyEvents(requestService);
        }

        // used to fetch user emails and names for the Invites autocomplete input field
        [HttpGet("users/byName")]
        public Response<IEnumerable<UserBasicDto>> GetUserByName([FromQuery] GetByNameRequest request)
        {
            return _userService.GetByName(request);
        }

        // POST methods
        [HttpPost("signup")]
        [AllowAnonymous]
        public Response<string> SignUp([FromBody] CreateUserRequest request)
        {
            return _userService.AddUser(request);
        }

        // Put methods
        [HttpPut("users/image")]
        public Response<bool> UploadPicture([FromForm] UploadProfilePictureRequest request)
        {

            return _userService.UploadPicture(request);
        }

        [HttpPut("users")]
        [Authorize(Policy = nameof(AuthorizationPolicyEnum.Admin))]
        public Response<bool> UpdateRole([FromBody] UpdateRoleRequest request)
        {
            return _userService.UpdateRole(request);
        }
        // Put methods
        [HttpPut("users/location")]
        public Response<bool> UploadLocation([FromBody] UploadLocationRequest request)
        {

            return _userService.UploadLocation(request);
        }


        // Delete methods
        [HttpDelete("users/image")]
        public Response<bool> RemoveProfilePicture([FromBody] RemoveProfilePictureRequest request)
        {
            return _userService.RemoveProfilePicture(request);
        }

    }
}
