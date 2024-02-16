using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Entities;

namespace GatherApp.Services.Extensions
{
    static class UserModelMapper
    {
        public static User SignupUserModel(this CreateUserRequest request, IPasswordService passwordService) 
        {
            string hashedPassword = passwordService.HashPassword(request.Password);

            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hashedPassword,
                CountryId = request.CountryId
            };
        }

        public static User SignupUserMicrosoftModel(this CreateUserSSORequest request)
        {
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Oid = request.Oid
            };
        }
    }
}
