using GatherApp.Contracts.Entities;
namespace GatherApp.Contracts.Requests
{
    public class CreateEventRequest : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public UploadFileRequest? Banner { get; set; }
        public string Type { get; set; }
        public string LocationUrl { get; set; }
        public string OrganizedBy { get; set; }
        public string Category { get; set; }
        public List<string> InviteesIds { get; set; } = new List<string>();
    }
}
