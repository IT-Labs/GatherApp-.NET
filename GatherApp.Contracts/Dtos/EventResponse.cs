namespace GatherApp.Contracts.Dtos
{
    public class EventResponse
    {
        public int TotalPageCount { get; set; }
        public int TotalItemCount { get; set; }
        public IEnumerable<SingleEventResponse> Events { get; set; } = Enumerable.Empty<SingleEventResponse>();
    }
}
