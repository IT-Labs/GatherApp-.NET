using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Contracts.Entities
{
    public class Email : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
