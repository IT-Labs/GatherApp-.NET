namespace GatherApp.Contracts.Requests
{
    public class GetEventRequestsRequest : GetByPageRequest
    {
        public string? Status { get; set; }
    }
}