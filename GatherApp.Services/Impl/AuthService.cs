using FluentValidation.Results;
using GatherApp.Contracts.Configuration;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Contracts.Validators;
using GatherApp.Repositories;
using GatherApp.Services.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace GatherApp.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;
        private readonly IHttpContextAccessor _context;
        private readonly Urls _urls;

        private readonly KeyValuePair<string, string> _oldRefreshTokenString;
        private readonly Token _currentRefreshToken;

        private readonly ILoggingService _loggingService;
        private readonly BlobStorageSettings _blobStorageSettings;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IPasswordService passwordService, IHttpContextAccessor context, Urls urls, ILoggingService loggingService, BlobStorageSettings blobStorageSettings)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _passwordService = passwordService;
            _context = context;
            _urls = urls;
            _loggingService = loggingService;
            _blobStorageSettings = blobStorageSettings;

            // get the refresh token value from the cookie received from the client side
            _oldRefreshTokenString = _context.HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == TokenEnum.RefreshToken.ToString());
            // get the token entity from the database
            _currentRefreshToken = _unitOfWork.TokenRepository.GetByValue(_oldRefreshTokenString.Value);
            
        }
        public Response<AuthResponseDto> Login(LoginUserRequest request)
        {
            User user = _unitOfWork.UserRepository.GetByEmail(request.Email);

            // check if user with that email exist, if not return User Not Found
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<AuthResponseDto>();
            }

            // verify user password
            var verifyPassword = _passwordService.VerifyPassword(request.Password, user.Password!);
            if (!verifyPassword)
            {
                return CustomResponseExtension.ResponseUnauthorized<AuthResponseDto>(Values.InvalidCredentialsErrorMessage);
            }

            var token = _jwtService.GenerateToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken(user);

            AuthResponseDto responseData = user.CreateAuthResponse(token.Token, token.Expires);

            // for security reasons we send the refresh token in an HttpOnly cookie
            CookieHandler.GenerateRefreshTokenCookie(refreshToken.Value, refreshToken.ExpiresAt, _context, _urls.ServerUrl);

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulLogin, responseData);
        }

        public Response<AuthResponseDto> LoginSSO(LoginUserSSORequest request)
        {
            var (firstName, lastName, email, oid) = AuthSSOToken.ExtractUserDataFromIdToken(request.IdToken);

            User user = _unitOfWork.UserRepository.GetByEmail(email);
            if (user == null)
            {
                var userRequest = new CreateUserSSORequest
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Oid = oid
                };

                CreateUserSSOValidator validator = new CreateUserSSOValidator();
                ValidationResult result = validator.Validate(userRequest);

                if (!result.IsValid)
                {
                    return CustomResponseExtension.ResponseBadRequest<AuthResponseDto>(Errors.GeneralError);
                }

                user = userRequest.SignupUserMicrosoftModel();

                user.RoleId = _unitOfWork.RoleRepository.GetRole(RoleEnum.User.ToString()).Id;
                user.ProfilePicture = $"{_blobStorageSettings.AzurePath}{Values.DefaultProfileImage}";
                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.Complete();
            }

            if(user.Oid == null)
            {
                user.Oid = oid;
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.Complete();
            }

            if (user.Oid != null && oid != user.Oid)
            {
                return CustomResponseExtension.ResponseUnauthorized<AuthResponseDto>(Values.InvalidCredentialsErrorMessage);
            }

            var token = _jwtService.GenerateToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken(user);

            AuthResponseDto response = user.CreateAuthResponse(token.Token, token.Expires);

            // for security reasons we send the refresh token in an HttpOnly cookie
            CookieHandler.GenerateRefreshTokenCookie(refreshToken.Value, refreshToken.ExpiresAt, _context, _urls.ServerUrl);

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulLogin, response);
        }

        public Response<RefreshTokenResponse> RefreshAccessToken()
        {
            var result = InvalidateRefreshToken();
            if (result is IResponse && result.Status != HttpStatusCode.OK) return result;

            // get user from the current refresh token
            var user = _currentRefreshToken.User;
            if (user == null)
            {
                return CustomResponseExtension.ResponseUnauthorized<RefreshTokenResponse>(Errors.Unauthorized);
            }

            var newAccessToken = _jwtService.GenerateToken(user);

            var newRefreshToken = _jwtService.GenerateRefreshToken(user);

            CookieHandler.GenerateRefreshTokenCookie(newRefreshToken.Value, newRefreshToken.ExpiresAt, _context, _urls.ServerUrl);

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", new RefreshTokenResponse
            {
                Token = newAccessToken.Token,
                Expires = newAccessToken.Expires,
            });
        }

        public Response<RefreshTokenResponse> InvalidateRefreshToken()
        {
            if (_oldRefreshTokenString.Value == null)
            {
                return CustomResponseExtension.ResponseUnauthorized<RefreshTokenResponse>(Errors.Unauthorized);
            }

            if (_currentRefreshToken == null || _currentRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                return CustomResponseExtension.ResponseUnauthorized<RefreshTokenResponse>(Errors.Unauthorized);
            }

            // invalidate the current refresh token by deleting it
            _unitOfWork.TokenRepository.HardDelete(_currentRefreshToken);
            _unitOfWork.Complete();

            return CustomResponseExtension.GenericResponse<RefreshTokenResponse>(HttpStatusCode.OK, Messages.SuccessfulLogout);
        }
    }
}
