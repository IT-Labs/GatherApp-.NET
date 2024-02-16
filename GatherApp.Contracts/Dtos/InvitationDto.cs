using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Dtos
{
    public class InvitationDto: Invitation
    {
        public string InvitationId { get; set; } = Guid.NewGuid().ToString();
        public string EventId { get; set; }
        public string UserId { get; set; }
        public InviteStatusEnum InviteStatus { get; set; }
    }
}
