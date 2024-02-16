namespace GatherApp.Contracts.Requests
{
    public class GetEventsRequest : GetByPageRequest
    {
        public string? Organizer { get; set; }
        public string? Type { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
