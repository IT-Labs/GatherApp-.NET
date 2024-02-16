
using GatherApp.Contracts.Entities.Interfaces;
using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Entities
{
    public class Invitation : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public InviteStatusEnum InviteStatus { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
