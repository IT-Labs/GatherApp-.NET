namespace GatherApp.Contracts.Dtos
{
    public class UserCalendarEventResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? Status { get; set; }
        public string? OrganizedBy { get; set; }
        public string? CreatedBy { get; set; }
    }
}
