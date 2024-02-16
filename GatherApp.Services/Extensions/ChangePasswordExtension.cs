using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Requests;

namespace GatherApp.Services.Extensions
{
    public class ChangePasswordExtension
    {
        public static User HandleUpdate(User response, ResetPasswordRequest request, IPasswordService passwordService)
        {
            response.Password = passwordService.HashPassword(request.NewPassword);
            return response;
        }
    }
}