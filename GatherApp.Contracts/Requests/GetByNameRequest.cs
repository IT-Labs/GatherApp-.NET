namespace GatherApp.Contracts.Requests
{
    public class GetByNameRequest : IRequest
    {
        public string? Name { get; set; } = string.Empty;
        public string? Countries { get; set; } = string.Empty;
    }
}
