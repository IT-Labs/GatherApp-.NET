
using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Requests
{
    public class UpdateInviteStatusRequest: IRequest
    {
        public string InvitationId { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; } 
        public InviteStatusEnum InviteStatus { get; set; }
    }
}
