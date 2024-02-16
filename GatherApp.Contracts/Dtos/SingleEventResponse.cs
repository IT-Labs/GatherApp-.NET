namespace GatherApp.Contracts.Dtos
{
    public class SingleEventResponse
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? Banner { get; set; }
        public string? Type { get; set; }
        public string? LocationUrl { get; set; }
        public string? OrganizedBy { get; set; }
        public bool? IsInviteOnly { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public ICollection<EventInvitationDto>? Invitations { get; set; }
        public string CreatedBy { get; set; }
        public string? CreatedByEmail { get; set; }
    }
}
