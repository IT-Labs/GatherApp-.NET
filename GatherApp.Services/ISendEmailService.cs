using GatherApp.Contracts.Dtos;

namespace GatherApp.Services
{
    public interface ISendEmailService
    {
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
