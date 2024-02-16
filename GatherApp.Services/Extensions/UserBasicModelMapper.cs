using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;

namespace GatherApp.Services.Extensions
{
    static class UserBasicModelMapper
    {
        public static UserBasicDto CreateUserBasicDtoResponse(this User user)
        {
            return new UserBasicDto()
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Id = user.Id,
                ProfilePicture = user.ProfilePicture
            };
        }
    }
}
