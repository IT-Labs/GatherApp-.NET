namespace GatherApp.Contracts.Requests
{
    public class CreateUserSSORequest : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Oid { get; set; }
    }
}
