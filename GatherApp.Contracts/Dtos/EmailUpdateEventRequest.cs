using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Dtos
{
    public class EmailUpdateEventRequest : IDefaultEmailRequest
    {
        public string To { get; set; } = string.Empty;
        public EmailEnum EmailEnum { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string EventLink { get; set; }
    }
}
