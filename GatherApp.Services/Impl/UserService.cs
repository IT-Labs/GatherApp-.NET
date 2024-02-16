using FluentValidation.Results;
using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Contracts.Validators;
using GatherApp.Services.Extensions;
using GatherApp.Repositories;
using System.Net;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Expressions;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Configuration;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;

namespace GatherApp.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BlobStorageSettings _blobStorageSettings;
        private readonly IFileService _fileService;
        private readonly IPasswordService _passwordService;
        private readonly ILoggingService _loggingService;
        
        public UserService(IUnitOfWork unitOfWork, BlobStorageSettings blobStorageSettings, IFileService fileService, IPasswordService passwordService, ILoggingService loggingService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageSettings = blobStorageSettings;
            _fileService = fileService;
            _passwordService = passwordService;
            _loggingService = loggingService;
        }

        // this endpoint is used on the Admin panel page
        public Response<UserResponse> GetListOfUsers(ListOfUsersRequest request)
        {
            var result = new ListOfUsersRequestValidator().Validate(request);

            if (!result.IsValid)
            {
                return CustomResponseExtension.ResponseBadRequest<UserResponse>(Errors.GeneralError);
            }

            request.Role = string.IsNullOrWhiteSpace(request.Role) ? Values.RoleFilterAll : request.Role;
            request.Name = string.IsNullOrWhiteSpace(request.Name) ? string.Empty : request.Name;

            List<User> userEntities = _unitOfWork.UserRepository
                .GetUsersSuggestionsByNameAndRole(request.Name, request.Role).ToList();

            var response = new UserResponse
            {
                TotalPageCount = (int)Math.Ceiling(userEntities.Count() / (decimal)request.PageSize),
                TotalItemCount = userEntities.Count(),

                Users = userEntities.Select(UserExpression.MapToUserDto().Compile()).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList()
            };

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }

        public Response<SingleUserResponse> GetById(GetByIdRequest request)
        {
            var user = _unitOfWork.UserRepository.GetUserById(request.Id);

            if(user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<SingleUserResponse>();
            }

            var response = new SingleUserResponse();
            response = UserExpression.MapToUserDto().Compile()(user);

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }

        // get users by Name
        public Response<IEnumerable<UserBasicDto>> GetByName(GetByNameRequest request)
        {
            // create a List of countries from the request.Countries string
            List<string> countries = !request.Countries.IsNullOrEmpty() ? request.Countries.Split(",").ToList() : new List<string>();

            List<UserBasicDto> users = _unitOfWork.UserRepository.GetThreeUsersSuggestionsByName(request.Name.IsNullOrEmpty() ? "": request.Name, countries).Select(x => x.CreateUserBasicDtoResponse()).ToList();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", users.DistinctBy(x => x.Email));
        }

        // get all Events created by a user with the specified ID
        public Response<EventResponse> GetMyEvents(MyEventsServiceRequest request)
        {
            // read the token and get the user role from it
            var role = JWTClaimsReader.HandleJWTClaims(Values.ClaimRoleName);

            // grab all events created by the user with the specified ID and use all events as a basis in case the user role is Admin and filter Organizer is "Both" (Company + Individual)
            var events = _unitOfWork.EventRepository.GetMyEvents(request.Id).Where(ev => !ev.DeletedAt.HasValue);

            switch (request.EventStatus)
            {
                case Values.EventStatusApproved:
                    events = events.Where(ev => ev.Status == EventStatusEnum.Approved.ToString() && ev.DateStart >= DateTime.Now);
                    break;
                case Values.EventStatusPending:
                    events = events.Where(ev => ev.Status == EventStatusEnum.Pending.ToString() && ev.DateStart >= DateTime.Now);
                    break;
                case Values.EventStatusDeclined:
                    events = events.Where(ev => ev.Status == EventStatusEnum.Declined.ToString());
                    break;
                case Values.EventStatusPast:
                    events = events.Where(ev => ev.DateStart < DateTime.Now && ev.Status != EventStatusEnum.Declined.ToString()).OrderByDescending(ev => ev.DateStart);
                    break;
                default:
                    events = events.Where(ev => ev.DateStart >= DateTime.Now).OrderBy(ev => ev.DateStart);
                    break;
            }

            // if the filter is either Company or Individual, return only events with that OrganizedBy value
            var result = new MyEventsServiceRequestValidator().Validate(request);

            if (!result.IsValid)
            {
                return CustomResponseExtension.ResponseBadRequest<EventResponse>(Errors.InvalidOrganizedBy);
            }

            if (role == RoleEnum.Admin.ToString() && !string.IsNullOrWhiteSpace(request.Organizer))
            {
                events = events.Where(ev => ev.OrganizedBy == request.Organizer);
            }

            var response = new EventResponse
            {
                TotalPageCount = (int)Math.Ceiling(events.Count() / (decimal)request.PageSize),
                TotalItemCount = events.Count(),
                Events = events.Select(EventExpression.MapToEventDto().Compile()).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList().OrderBy(ev => ev.DateStart)
            };

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }
        
        public Response<string> AddUser(CreateUserRequest request)
        {
            var addUserValidator = new CreateUserValidator();
            var addUserValidatorResult = addUserValidator.Validate(request);

            if (!addUserValidatorResult.IsValid)
            {
                return addUserValidatorResult.CreateErrorsResponseWithString(HttpStatusCode.BadRequest);
            }

            var checkIfUserIsRegistered = _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (checkIfUserIsRegistered != null)
            {
                return CustomResponseExtension.ResponseBadRequest<string>(Errors.EmailAlreadyExists);
            }

            User user = request.SignupUserModel(_passwordService);

            user.RoleId = _unitOfWork.RoleRepository.GetRole(RoleEnum.User.ToString()).Id;
            user.ProfilePicture = $"{_blobStorageSettings.AzurePath}{Values.DefaultProfileImage}";
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulSignUp, user.Id);
        }

        public Response<bool> UpdatePassword(ResetPasswordRequest request)
        {
            var updatePasswordValidator = new ChangePasswordValidator();
            var updatePasswordValidatorResult = updatePasswordValidator.Validate(request);

            if (!updatePasswordValidatorResult.IsValid)
            {
                return updatePasswordValidatorResult.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
            }

            var token = _unitOfWork.PasswordRepository.GetByToken(request.Token);

            if (token == null)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.TokenNotFound);
            }

            var expiresAt = token.ExpiresAt;
            var currentTime = DateTime.UtcNow;
            if (currentTime > expiresAt)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.TokenIsExpired);
            }

            if (token.IsUsed == true)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.TokenIsUsed);
            }

            var user = _unitOfWork.UserRepository.Get(token.UserId);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<bool>();
            }

            var updateUser = ChangePasswordExtension.HandleUpdate(user, request, _passwordService);
            token.IsUsed = true;

            _unitOfWork.UserRepository.Update(updateUser);
            _unitOfWork.PasswordRepository.Update(token);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulPasswordChange, true);
        }

        public Response<bool> UploadPicture(UploadProfilePictureRequest request)
        {
            if (request.Picture == null)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.NoProfileImage);
            }

            var uploadPictureValidator = new ImageValidator();
            var uploadPictureValidatorResult = uploadPictureValidator.Validate(request.Picture);

            if (!uploadPictureValidatorResult.IsValid)
            {
                return uploadPictureValidatorResult.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
            }

            var user = _unitOfWork.UserRepository.Get(request.Id);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<bool>();
            }

            var userId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
            if (userId != user.Id)
            {
                return CustomResponseExtension.ResponseInternalServerError<bool>(Values.InternalServerErrorMessage);
            }

            var extension = Path.GetExtension(request.Picture.ImageFile.FileName);
            var path = Values.PathForUsers + user.Id + DateTime.Now.ToString("HH:mm:ss") + extension;

            var status = _fileService.Upload(request.Picture, path);
            if (status.Data == false)
            {
                return CustomResponseExtension.ResponseInternalServerError<bool>(Values.InternalServerErrorMessage);
            }

            user.ProfilePicture = $"{_blobStorageSettings.AzurePath}{path}";

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulProfilePhotoUpload, true);
        }

        public Response<bool> UpdateRole(UpdateRoleRequest request)
        {
            var updateRoleValidator = new UpdateRoleValidator();
            var updateRoleValidatorResult = updateRoleValidator.Validate(request);

            if(!updateRoleValidatorResult.IsValid)
            {
                return updateRoleValidatorResult.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
            }

            // user can't change own role
            var changerUserId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
            if (request.UserId == changerUserId)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.UnexpectedError);
            }

            var user = _unitOfWork.UserRepository.Get(request.UserId);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<bool>();
            }

            user.Role = _unitOfWork.RoleRepository.Get(user.RoleId);
            if(user.Role.RoleName == request.Role)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Messages.SameRole);
            }

            var newRole = _unitOfWork.RoleRepository.GetRole(request.Role);
            if (newRole == null)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.InvalidRole);
            }

            user.Role = newRole;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulRoleUpdate, true);
        }

        public Response<bool> ChangePassword(ChangePasswordRequest request)
        {
            var changePasswordValidator = new PasswordValidator();
            var changePasswordValidatorResult = changePasswordValidator.Validate(request);

            if (!changePasswordValidatorResult.IsValid)
            {
                return changePasswordValidatorResult.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
            }

            var userId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
            var user = _unitOfWork.UserRepository.GetUserById(userId);

            // checking for invalid logging (user tries different ID)
            if (user == null || userId != request.Id)
            {
                return CustomResponseExtension.ResponseUnauthorized<bool>(Errors.Unauthorized);
            }

            // check if current password is correct
            var verifyCurrentPassword = _passwordService.VerifyPassword(request.OldPassword, user.Password!);
            if (!verifyCurrentPassword)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.CurrentPasswordMismatch);
            }

            // check if new password is same as old
            if (request.NewPassword == request.OldPassword)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.NewPasswordSameAsOld);
            }

            user.Password = _passwordService.HashPassword(request.NewPassword);

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulPasswordChange, true);
        }

        public Response<bool> UploadLocation(UploadLocationRequest request)
        {
            var user = _unitOfWork.UserRepository.GetUserById(request.Id);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<bool>();
            }

            var userId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
            if (userId != request.Id)
            {
                return CustomResponseExtension.ResponseUnauthorized<bool>(Errors.UserNotAuthenticated);
            }

            if (request.Id == null)
            {
                return CustomResponseExtension.ResponseBadRequest<bool>(Errors.InvalidCountry);
            }

            user.CountryId = request.CountryId;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulCountryUpdate, true);
        }

        public Response<bool> RemoveProfilePicture(RemoveProfilePictureRequest request)
        {
            var user = _unitOfWork.UserRepository.GetUserById(request.Id);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<bool>();
            }

            var userId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
            if(userId != request.Id)
            {
                return CustomResponseExtension.ResponseUnauthorized<bool>(Errors.UserNotAuthenticated);
            }

            user.ProfilePicture = $"{_blobStorageSettings.AzurePath}{Values.DefaultProfileImage}";

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Complete();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulProfilePhotoRemove, true);
        }
    }}
