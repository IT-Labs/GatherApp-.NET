using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;

namespace GatherApp.Services.Extensions
{
    public static class AddInvitationExtension
    {
        public static InvitationDto ToInvitationDto(this Event eventObj, User userObj, InviteStatusEnum inviteStatus)
        {
            return new InvitationDto
            {
                EventId = eventObj.Id,
                UserId = userObj.Id,
                InviteStatus = inviteStatus,
                User = userObj,
                Event = eventObj
            };
        }
    }
}
