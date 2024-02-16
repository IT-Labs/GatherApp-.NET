using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GatherApp.API.Controllers
{
    [ApiController]
    [Authorize]
    public class PasswordController : ControllerBase
    {
        private readonly IValidateEmailService _emailService;
        private readonly IUserService _userService;

        public PasswordController(IValidateEmailService emailService, IUserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public Response<bool> ForgotPassword(EmailRequest request)
        {
            return _emailService.ValidateEmail(request);
        }

        [HttpPut("reset-password/{token}")]
        [AllowAnonymous]
        public Response<bool> ResetPassword(string token, [FromBody] ResetPasswordRequest request)
        {
            request.Token = token;
       
            return _userService.UpdatePassword(request);
        }

        [HttpPut("change-password")]
        public Response<bool> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            return _userService.ChangePassword(request);
        }

    }
}
