using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Dtos
{
    public class EmailCancelEventRequest : IDefaultEmailRequest
    {
        public string To { get; set; } = string.Empty;
        public EmailEnum EmailEnum { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Organizer { get; set; }
        public string DateStart { get; set; }
        public string TimeStart { get; set; }
    }
}
