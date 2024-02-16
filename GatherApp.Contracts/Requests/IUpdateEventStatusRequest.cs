namespace GatherApp.Contracts.Requests
{
    public interface IUpdateEventStatusRequest
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
