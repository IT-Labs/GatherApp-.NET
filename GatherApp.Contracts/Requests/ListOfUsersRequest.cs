namespace GatherApp.Contracts.Requests
{
    public class ListOfUsersRequest : GetByPageRequest
    {
        public string? Role { get; set; }
        public string? Name { get; set; }
    }
}
