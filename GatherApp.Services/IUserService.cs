using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GatherApp.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a list of users based on the specified request.
        /// </summary>
        /// <param name="request">The request containing criteria (e.g. Role, Name) for retrieving the list of users.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="UserResponse"/> representing the list of users.</returns>
        Response<UserResponse> GetListOfUsers(ListOfUsersRequest request);

        /// <summary>
        /// Retrieves user details based on the specified request.
        /// </summary>
        /// <param name="request">The request containing the unique identifier (Id) of the user to retrieve.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="SingleUserResponse"/> representing the details of the requested user.</returns>
        Response<SingleUserResponse> GetById(GetByIdRequest request);

        /// <summary>
        /// Adds a new user based on the provided request.
        /// </summary>
        /// <param name="request">The request containing information to create a new user.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="string"/> representing the unique identifier of the newly created user.</returns>
        Response<string> AddUser(CreateUserRequest request);

        /// <summary>
        /// Updates the password for a user based on the provided reset password request.
        /// </summary>
        /// <param name="request">The request containing information to update the user's password.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the password update was successful.</returns>
        public Response<bool> UpdatePassword(ResetPasswordRequest request);

        /// <summary>
        /// Retrieves events associated with the user specified by the provided user ID.
        /// </summary>
        /// <param name="request">The request containing the user ID for which to retrieve events.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with an <see cref="EventResponse"/> representing the events associated with the specified user.</returns>
        Response<EventResponse> GetMyEvents(MyEventsServiceRequest request);

        /// <summary>
        /// Retrieves user information based on the specified request by name.
        /// </summary>
        /// <param name="request">The request containing criteria for retrieving users by name.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with an <see cref="IEnumerable{UserBasicDto}"/> representing user information.</returns>
        Response<IEnumerable<UserBasicDto>> GetByName(GetByNameRequest request);

        /// <summary>
        /// Changes the password for the current user based on the provided change password request.
        /// </summary>
        /// <param name="request">The request containing information to change the user's password.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the password change was successful.</returns>
        public Response<bool> ChangePassword(ChangePasswordRequest request);

        /// <summary>
        /// Uploads a profile picture for the current user based on the provided request.
        /// </summary>
        /// <param name="request">The request containing the profile picture to be uploaded.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the upload was successful.</returns>
        Response<bool> UploadPicture(UploadProfilePictureRequest request);

        /// <summary>
        /// Uploads location information based on the provided request.
        /// </summary>
        /// <param name="request">The request containing the location information to be added.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the location add was successful.</returns>
        Response<bool> UploadLocation([FromForm] UploadLocationRequest request);

        /// <summary>
        /// Removes the profile picture for the user specified by the provided request.
        /// </summary>
        /// <param name="request">The request containing the user ID for which to remove the profile picture.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the removal was successful.</returns>
        Response<bool> RemoveProfilePicture(RemoveProfilePictureRequest request);

        /// <summary>
        /// Updates the role for a user based on the provided role update request.
        /// </summary>
        /// <param name="request">The request containing information to update the user's role.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the role update was successful.</returns>
        Response<bool> UpdateRole(UpdateRoleRequest request);

    }
}
