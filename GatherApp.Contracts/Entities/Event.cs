using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Contracts.Entities
{
    public class Event : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? Banner { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? LocationUrl { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? OrganizedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsInviteOnly { get; set; }
        public string Category { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
