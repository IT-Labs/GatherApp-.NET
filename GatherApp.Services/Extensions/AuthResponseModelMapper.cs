using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;

namespace GatherApp.Services.Extensions
{
    static class AuthResponseModelMapper
    {
        public static AuthResponseDto CreateAuthResponse(this User userObj, string token, DateTime expirationDate)
        {
            return new AuthResponseDto
            {
                Token = token,
                User = userObj.CreateUserResponseDto(),
                ExpirationDate = expirationDate
            };
        }
    }

}
