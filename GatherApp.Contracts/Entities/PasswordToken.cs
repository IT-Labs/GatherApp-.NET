using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Contracts.Entities
{
    public class PasswordToken : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(1);
        public string UserId { get; set; }
        public bool IsUsed { get; set; } = false;
        
        public PasswordToken(string userId) 
        {
            UserId = userId;
        }
    }
}
