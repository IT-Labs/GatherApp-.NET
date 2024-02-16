using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Dtos
{
    public class EmailEventInviteRequest : IDefaultEmailRequest
    {
        public string To { get; set; } = string.Empty;
        public EmailEnum EmailEnum { get; set; }
        public string Subject { get; set; }
        public string? Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public string Type { get; set; }
        public string DateStart { get; set; }
        public string TimeStart { get; set; }
        public string DateEnd { get; set; }
        public string TimeEnd { get; set; }
        public string Category { get; set; }
        public string EventLink { get; set; }
        public bool IsOnSite { get; set; }
    }
}
