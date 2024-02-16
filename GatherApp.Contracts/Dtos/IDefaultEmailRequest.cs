using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Dtos
{
    public interface IDefaultEmailRequest
    {
        public string To { get; set; }
        public EmailEnum EmailEnum { get; set; }
        public string Subject { get; set; }
    }
}
