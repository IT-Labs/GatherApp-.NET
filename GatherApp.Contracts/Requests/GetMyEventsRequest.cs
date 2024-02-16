namespace GatherApp.Contracts.Requests
{
    public class GetMyEventsRequest : GetByPageRequest
    {
        public string? Organizer { get; set; }
        public string EventStatus { get; set; }
    }
}
