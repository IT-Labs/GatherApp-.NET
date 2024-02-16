using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Contracts.Entities
{
    public class User : IEntity<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? CountryId { get; set; }
        public Country? Country { get; set; }
        public string? Oid { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? EditedBy { get; set; }
        public string? DeletedBy { get; set; }
        public Role Role { get; set; } 
        public string RoleId { get; set; }
        public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
        //Many-to-many
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
