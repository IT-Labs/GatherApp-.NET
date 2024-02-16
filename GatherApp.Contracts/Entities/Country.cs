using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Contracts.Entities
{
    public class Country : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
