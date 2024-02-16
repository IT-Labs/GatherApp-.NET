namespace GatherApp.Contracts.Requests
{
    public class DeclineEventStatusRequest : IUpdateEventStatusRequest
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string? DeclineReason { get; set; }
    }
}
