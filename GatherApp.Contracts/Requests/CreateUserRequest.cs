namespace GatherApp.Contracts.Requests
{
    public class CreateUserRequest : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryId { get; set; }
        public string Password { get; set; }
    }
}
