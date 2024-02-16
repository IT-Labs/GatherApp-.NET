namespace GatherApp.Contracts.Dtos
{
    public class SingleUserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public string RoleName { get; set; }
    }
}
