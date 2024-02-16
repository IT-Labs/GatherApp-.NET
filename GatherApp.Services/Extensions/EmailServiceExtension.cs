using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Requests;
using GatherApp.Repositories;
using System.Text.RegularExpressions;

namespace GatherApp.Services.Extensions
{
    public static class EmailServiceExtension
    {
        public static EmailUpdateEventRequest SendEmailToCreatorForUpdateEvent(this User user, Event eventObject, string eventLink, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailUpdateEventRequest()
                {
                    To = user.Email,
                    Subject = ReplaceEventEmailSubject(eventObject, EmailEnum.UpdatedEvent, emailRepository),
                    Title = eventObject.Title,
                    EventLink = eventLink,
                    EmailEnum = EmailEnum.UpdatedEvent
                };

                return email;
            }
            catch 
            {
                return new EmailUpdateEventRequest();
            }
        }

        public static EmailUpdateEventRequest SendEmailToInviteesForUpdateEvent(this Invitation invitation, Event eventObject, string eventLink, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailUpdateEventRequest()
                {
                    To = invitation.User.Email,
                    Subject = ReplaceEventEmailSubject(eventObject, EmailEnum.UpdatedEvent, emailRepository),
                    Title = eventObject.Title,
                    EventLink = eventLink,
                    EmailEnum = EmailEnum.UpdatedEvent
                };

                return email;
            }
            catch 
            {
                return new EmailUpdateEventRequest();
            }
        }

        public static EmailDeclineEventRequest SendEmailForDeclineEvent(this User user, Event eventObject, string eventLink, string declineReason, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailDeclineEventRequest()
                {
                    To = user.Email,
                    Subject = ReplaceEventEmailSubject(eventObject, EmailEnum.DeclinedEvent, emailRepository),
                    DeclineReason = string.IsNullOrEmpty(declineReason) ? "Not specified." : declineReason,
                    Title = eventObject.Title,
                    EventLink = eventLink,
                    EmailEnum = EmailEnum.DeclinedEvent
                };

                return email;
            }
            catch
            {
                return new EmailDeclineEventRequest();
            }
        }

        public static EmailCancelEventRequest SendEmailForCancelEvent(this Invitation invitation, Event eventObject, string organizer, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailCancelEventRequest()
                {
                    To = invitation.User.Email,
                    Subject = ReplaceEventEmailSubject(eventObject, EmailEnum.CancelledEvent, emailRepository),
                    Title = eventObject.Title,
                    Organizer = organizer,
                    DateStart = eventObject.DateStart.ToString().Split(" ")[0],
                    TimeStart = eventObject.DateStart.ToString().Split(" ")[1],
                    EmailEnum = EmailEnum.CancelledEvent
                };

                return email;
            }
            catch
            {
                return new EmailCancelEventRequest();
            }
        }

        public static EmailEventInviteRequest SendEmailToInvitees(this User user, Event eventObject, string eventLink, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailEventInviteRequest()
                {
                    To = user.Email,
                    Subject = ReplaceEventEmailSubject(eventObject, EmailEnum.EmailInvite, emailRepository),
                    Title = eventObject.Title,
                    Description = ShortenEventDescription(eventObject.Description),
                    Banner = eventObject.Banner,
                    Type = eventObject.Type,
                    DateStart = eventObject.DateStart.ToString().Split(" ")[0],
                    TimeStart = eventObject.DateStart.ToString().Split(" ")[1],
                    DateEnd = eventObject.DateEnd.ToString().Split(" ")[0],
                    TimeEnd = eventObject.DateEnd.ToString().Split(" ")[1],
                    Location = eventObject.LocationUrl,
                    Category = eventObject.Category,
                    EventLink = eventLink,
                    EmailEnum = EmailEnum.EmailInvite
                };

                if (email.Type == Values.TypeOnSite)
                    email.IsOnSite = true;

                return email;
            }
            catch 
            { 
                return new EmailEventInviteRequest();
            }
        }

        public static EmailEventDeleteInviteRequest SendEmailToDeletedInvitees(this User user, Event eventObject, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailEventDeleteInviteRequest()
                {
                    To = user.Email,
                    Subject = ReplaceEventEmailSubject(eventObject, EmailEnum.EmailDeletedInvite, emailRepository),
                    Title = eventObject.Title,
                    EmailEnum = EmailEnum.EmailDeletedInvite
                };

                return email;
            }
            catch
            {
                return new EmailEventDeleteInviteRequest();
            }
        }

        public static EmailResetPasswordRequest SendEmailForPasswordReset(this User user, string resetLink, EmailRequest request, IEmailRepository emailRepository)
        {
            try
            {
                var email = new EmailResetPasswordRequest()
                {
                    To = request.To,
                    Subject = ReplaceGenericEmailSubject(EmailEnum.ForgotPassword, emailRepository),
                    ResetLink = resetLink,
                    EmailEnum = EmailEnum.ForgotPassword
                };

                return email;
            }
            catch
            {
                return new EmailResetPasswordRequest();
            }
        }

        #region Helper Classes

        private static string StripHtmlTagsFromEventDescription(string eventDescription)
        {
            return Regex.Replace(eventDescription, "<.*?>", string.Empty);
        }

        private static string ShortenEventDescription(string eventDescription)
        {
            string cleanDescription = StripHtmlTagsFromEventDescription(eventDescription);
            return cleanDescription.Length > 155 ? $"{cleanDescription.Substring(0, 155)}..." : cleanDescription;
        }

        private static string ReplaceEventEmailSubject(Event eventObject, EmailEnum type, IEmailRepository emailRepository)
        {
            var email = emailRepository.GetEmailTemplate(type);

            if (email == null)
                return new string("No Subject");

            return new string($"{email.Subject} {eventObject.Title}");
        }

        private static string ReplaceGenericEmailSubject(EmailEnum type, IEmailRepository emailRepository)
        {
            var email = emailRepository.GetEmailTemplate(type);

            if (email == null)
                return new string("No Subject");

            return new string($"{email.Subject}");
        }

        #endregion
    }
}
