using GatherApp.Contracts.Dtos;

namespace GatherApp.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Renders an email template for a generic response object that implements the IDefaultEmailResponse interface.
        /// </summary>
        /// <typeparam name="T">Type of the response object implementing IDefaultEmailResponse.</typeparam>
        /// <param name="response">The response object containing data for rendering the email template.</param>
        /// <returns>The rendered email template as a string.</returns>
        /// <remarks>
        /// The IDefaultEmailResponse interface defines the basic structure expected for email response objects.
        /// </remarks>
        string RenderEmailTemplate<T>(T response) where T : IDefaultEmailRequest;

        /// <summary>
        /// Sends an email based on the provided email response.
        /// </summary>
        /// <typeparam name="T">Type of the email response object implementing IDefaultEmailResponse.</typeparam>
        /// <param name="response">The email response object containing details such as recipient, body, etc.</param>
        /// <remarks>
        /// This method is responsible for sending emails using the information provided in the specified email response.
        /// </remarks>
        public void SendEmail<T>(T response) where T : IDefaultEmailRequest;
    }
}
