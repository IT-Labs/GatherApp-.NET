using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Contracts.Entities
{
    public class Role : IEntity<string>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
