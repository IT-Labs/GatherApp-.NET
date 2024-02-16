using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;

namespace GatherApp.Repositories
{
    public interface IEmailRepository : IRepository<Email>
    {
        /// <summary>
        /// Retrieves an email template based on the specified email type.
        /// </summary>
        /// <param name="emailEnum">The enum representing the type of email template to retrieve.</param>
        /// <returns>An <see cref="Email"/> object representing the email template for the specified type.</returns>
        Email GetEmailTemplate(EmailEnum emailEnum);
    }
}
