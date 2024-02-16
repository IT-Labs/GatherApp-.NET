using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;

namespace GatherApp.Services
{
    public interface IValidateEmailService
    {
        /// <summary>
        /// Validates an email based on the provided request.
        /// </summary>
        /// <param name="request">The email request containing the email address to validate.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the email validation, with a <see cref="bool"/> indicating whether the email is valid.</returns>
        public Response<bool> ValidateEmail(EmailRequest request);
    }
}
