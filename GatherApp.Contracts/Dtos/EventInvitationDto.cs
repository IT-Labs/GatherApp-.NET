namespace GatherApp.Contracts.Dtos
{
    public class EventInvitationDto
    {
        public string? Id { get; set; }
        public string? InviteStatus { get; set; }
        public UserBasicDto? User { get; set; }
    }
}
