using GatherApp.Contracts.Configuration;
using GatherApp.Contracts.Dtos;
using GatherApp.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Stubble.Core;
using Stubble.Core.Builders;


namespace GatherApp.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly StubbleVisitorRenderer _stubbleRenderer;
        private readonly EmailSettings _emailSettings;
        private readonly IEmailRepository _emailRepository;

        public EmailService(EmailSettings emailSettings, IEmailRepository emailRepository)
        {
            _emailSettings = emailSettings;
            _stubbleRenderer = new StubbleBuilder().Build();
            _emailRepository = emailRepository;
        }

        public string RenderEmailTemplate<T>(T request) where T : IDefaultEmailRequest
        {
            var emailTemplate = _emailRepository.GetEmailTemplate(request.EmailEnum);
            return _stubbleRenderer.Render(emailTemplate.Body, request);
        }

        public void SendEmail<T>(T request) where T : IDefaultEmailRequest
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.Username));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = RenderEmailTemplate(request)
            };

            var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_emailSettings.Host, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
