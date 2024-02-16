using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatherApp.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public Response<AuthResponseDto> Login([FromBody] LoginUserRequest request)
        {
            return _authService.Login(request);
        }

        [HttpPost("refresh-access")]
        public Response<RefreshTokenResponse> RefreshAccessToken()
        {
            return _authService.RefreshAccessToken();
        }

        [HttpDelete("refresh-access")]
        public Response<RefreshTokenResponse> InvalidateRefreshToken()
        {
            return _authService.InvalidateRefreshToken();
        }
        [HttpPost("login/sso")]
        public Response<AuthResponseDto> LoginSSO([FromBody] LoginUserSSORequest request)
        {
            return _authService.LoginSSO(request);
        }

    }
}
