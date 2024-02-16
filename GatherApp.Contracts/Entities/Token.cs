using GatherApp.Contracts.Entities.Interfaces;
using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Entities
{
    public class Token : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Value { get; set; }
        public TokenEnum Type { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
