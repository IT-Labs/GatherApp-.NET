using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Responses;
using GatherApp.Contracts.Validators;
using GatherApp.Repositories;
using GatherApp.Services.Extensions;
using System.Net;
using GatherApp.Contracts.Configuration;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Constants;

namespace GatherApp.Services.Impl
{
    public class ValidateEmailService : IValidateEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailRepository _emailRepository;
        private readonly Urls _urls;
        private readonly IEmailService _emailService;

        public ValidateEmailService(IUnitOfWork unitOfWork, Urls urls, IEmailService emailService, IEmailRepository emailRepository)
        {
            _unitOfWork = unitOfWork;
            _urls = urls;
            _emailService = emailService;
            _emailRepository = emailRepository;
        }

        public Response<bool> ValidateEmail(EmailRequest request)
        {
            var emailValidator = new EmailValidator();

            var result = emailValidator.Validate(request);

            if (!result.IsValid)
            {
                return result.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
            }

            var user = _unitOfWork.UserRepository.GetByEmail(request.To);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<bool>();
            }

            var passwordToken = new PasswordToken(user.Id);
            _unitOfWork.PasswordRepository.Add(passwordToken);
            _unitOfWork.Complete();

            var resetLink = _urls.ResetPasswordUrl + passwordToken.Token;

            var email = user.SendEmailForPasswordReset(resetLink, request, _emailRepository);
            _emailService.SendEmail(email);

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.ForgotPasswordSubmission, true);
        }
    }
}
