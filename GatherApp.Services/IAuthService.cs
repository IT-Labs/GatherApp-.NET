using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;

namespace GatherApp.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user using the provided credentials.
        /// </summary>
        /// <param name="request">The request containing user login credentials.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the login operation, with an <see cref="AuthResponseDto"/> containing authentication information.</returns>
        Response<AuthResponseDto> Login(LoginUserRequest request);

        /// <summary>
        /// Authenticates a user using Microsoft Single Sign-On (SSO) credentials.
        /// </summary>
        /// <param name="request">The request containing user Microsoft SSO credentials.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the Microsoft SSO login operation, with an <see cref="AuthResponseDto"/> containing authentication information.</returns>
        Response<AuthResponseDto> LoginSSO(LoginUserSSORequest request);

        /// <summary>
        /// Refreshes the access token to extend the user's authenticated session.
        /// </summary>
        /// <returns>A <see cref="Response{T}"/> containing the refreshed access token, with a <see cref="RefreshTokenResponse"/> containing the refresh token information.</returns>
        Response<RefreshTokenResponse> RefreshAccessToken();

        /// <summary>
        /// Invalidates the current refresh token to log the user out.
        /// </summary>
        /// <returns>A <see cref="Response{T}"/> containing the result of the token invalidation operation, with a <see cref="RefreshTokenResponse"/> containing the invalidated refresh token information.</returns>
        Response<RefreshTokenResponse> InvalidateRefreshToken();
    }
}
