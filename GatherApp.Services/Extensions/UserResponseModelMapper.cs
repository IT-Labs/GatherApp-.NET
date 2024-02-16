using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;

namespace GatherApp.Services.Extensions
{
    public static class UserResponseModelMapper
    {
        public static SingleUserResponse CreateUserResponseDto(this User user)
        {
            return new SingleUserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CountryId = user.CountryId,
                CountryName = user.Country?.Name,
                ProfilePicture = user.ProfilePicture,
                CreatedAt = user.CreatedAt,
                EditedAt = user.EditedAt,
                RoleName = user.Role.RoleName
            };
        }
    }
}
