namespace GatherApp.Contracts.Requests
{
    public class ApproveEventStatusRequest : IUpdateEventStatusRequest
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
