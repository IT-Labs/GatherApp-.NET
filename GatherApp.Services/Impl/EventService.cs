using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Expressions;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Repositories;
using System.Net;
using GatherApp.Services.Extensions;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Configuration;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using GatherApp.Contracts.Validators;

namespace GatherApp.Services.Impl
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly BlobStorageSettings _blobStorageSettings;
        private readonly Urls _urls;


        public EventService(IUnitOfWork unitOfWork, IFileService fileService, BlobStorageSettings blobStorageSettings, 
            IUserRepository userRepository,Urls urls, IEmailService emailService, IEmailRepository emailRepository)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _blobStorageSettings = blobStorageSettings;
            _userRepository = userRepository;
            _urls = urls;
            _emailService = emailService;
            _emailRepository = emailRepository;
        }

        public Response<SingleEventResponse> GetById(GetByIdRequest request)
        {
            var eventObject = _unitOfWork.EventRepository.Get(request.Id);
            if (eventObject == null || eventObject.DeletedAt != null)
            {
                return CustomResponseExtension.ResponseEventNotFound<SingleEventResponse>();
            }

            eventObject.Invitations = _unitOfWork.InvitationRepository.GetAllEventInvites(eventObject.Id).ToList();

            var response = EventExpression.MapToEventDto().Compile()(eventObject);

            var user = _unitOfWork.UserRepository.Get(eventObject.CreatedBy);
            if (user == null)
            {
                response.CreatedBy = Values.EventCreatedByGuest;
                response.CreatedByEmail = Values.EventCreatedByGuest;
            } else
            {
                response.CreatedBy = user.FirstName + " " + user.LastName;
                response.CreatedByEmail = user.Email;
            }

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }

        public Response<IEnumerable<EventInvitationDto>> GetInvitations(GetByIdRequest request)
        {
            var invitations = _unitOfWork.InvitationRepository.GetAllEventInvites(request.Id).ToList();

            foreach (var invitation in invitations)
            {
                User user = _unitOfWork.UserRepository.Get(invitation.UserId);
                if (user != null)
                {
                    invitation.User = user;
                }
            }

            var response = invitations.Select(EventExpression.MapToEventInvitationDto().Compile()).ToList();

            return CustomResponseExtension.ResponseDataObject<IEnumerable<EventInvitationDto>>(HttpStatusCode.OK, "", response);
        }

        public Response<EventResponse> GetEvents(GetEventsRequest request)
        {
            var events = _unitOfWork.EventRepository.GetAll();

            // remove deleted events and past events for both Home and Requests pages
            events = events.Where(ev => (ev.DeletedAt == null) && (ev.DateStart >= DateTime.Now));

            // return only approved events for the Home page
            events = events.Where(ev => ev.Status == EventStatusEnum.Approved.ToString());

            // filtering
            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                events = events.Where(ev => ev.Category == request.Category);
            }

            if (!string.IsNullOrWhiteSpace(request.Location))
            {
                events = events.Where(ev => ev.LocationUrl.Contains(request.Location));
            }

            if (!string.IsNullOrWhiteSpace(request.Type))
            {
                events = events.Where(ev => ev.Type == request.Type);
            }

            if (!string.IsNullOrWhiteSpace(request.Organizer))
            {
                events = events.Where(ev => ev.OrganizedBy == request.Organizer);
            }

            if (request.StartDate.HasValue)
            {
                // start date is provided
                var startDateOnly = request.StartDate.Value.Date;

                events = events.Where(ev => ev.DateEnd.Date >= startDateOnly);
            }

            if (request.EndDate.HasValue)
            {
                // end date is provided
                var endDateOnly = request.EndDate.Value.Date;

                events = events.Where(ev => ev.DateStart.Date <= endDateOnly);
            }

            events = events.OrderBy(ev => ev.DateStart);

            EventInvitations.GetEventInvitations(_unitOfWork, events);

            var response = new EventResponse
            {
                TotalPageCount = (int)Math.Ceiling(events.Count() / (decimal)request.PageSize),
                TotalItemCount = events.Count(),

                Events = events.Select(EventExpression.MapToEventDto().Compile()).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList()
            };

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }

        public Response<EventResponse> GetEventRequests(GetEventRequestsRequest request)
        {
            // get all events that aren't deleted or past events
            var events = _unitOfWork.EventRepository.GetAll().Where(ev => (ev.DeletedAt == null) && (ev.DateStart >= DateTime.Now));

            // we don't return declined events and wrong status requests
            if (request.Status.IsNullOrEmpty()
                || request.Status == EventStatusEnum.Declined.ToString()
                || !Enum.IsDefined(typeof(EventStatusEnum), request.Status))
            {
                return CustomResponseExtension.ResponseBadRequest<EventResponse>(Errors.InvalidStatus);
            } 

            events = events.Where(ev => ev.Status == request.Status).OrderBy(ev => ev.DateStart);

            var response = new EventResponse
            {
                TotalPageCount = (int)Math.Ceiling(events.Count() / (decimal)request.PageSize),
                TotalItemCount = events.Count(),

                Events = events.Select(EventExpression.MapToEventDto().Compile()).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList()
            };

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }

        public Response<bool> DeleteById(GetByIdRequest request)
        {
            var eventObject = _unitOfWork.EventRepository.Get(request.Id);
            if (eventObject == null)
            {
                return CustomResponseExtension.ResponseEventNotFound<bool>();
            }

            User user = _unitOfWork.UserRepository.GetUserById(eventObject.CreatedBy);
            string organizer = user.FirstName + " " + user.LastName;

            eventObject.DeletedAt = DateTime.UtcNow;       
            _unitOfWork.EventRepository.Delete(eventObject);
            _unitOfWork.Complete();

            var invitations = _unitOfWork.InvitationRepository.GetAllEventInvites(request.Id).Where(invitation => invitation.InviteStatus != InviteStatusEnum.NotGoing).ToList();
            SendCancelEventEmailToUsers(invitations, eventObject, organizer, _emailRepository);

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulDelete, true);
        }

        public Response<bool> UpdateEvent(string eventId, UpdateEventRequest request)
        {
            var updateEventValidator = new UpdateEventValidator();
            var updateEventValidatorResult = updateEventValidator.Validate(request);

            if (!updateEventValidatorResult.IsValid)
            {
                return updateEventValidatorResult.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
            }

            string requestUserRole = JWTClaimsReader.HandleJWTClaims(Values.ClaimRoleName);
            if (requestUserRole == RoleEnum.User.ToString())
            {
                if (request.OrganizedBy == OrganizedByEnum.Company.ToString())
                {
                    return CustomResponseExtension.ResponseBadRequest<bool>(Errors.InvalidOrganizatorForUserRole);
                }
            }

            var existingEventObject = _unitOfWork.EventRepository.Get(eventId);
            if (existingEventObject == null)
            {
                return CustomResponseExtension.ResponseEventNotFound<bool>();
            }

            string eventLink = GetEventUrl(existingEventObject);

            DateTime startDate = existingEventObject.DateStart;
            DateTime endDate = existingEventObject.DateEnd;

            try
            {
                var eventObject = HandleUpdateExtension.HandleUpdate(existingEventObject, request);
                eventObject.EditedBy = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);

                // checks if the user who is editing the event is an admin and is not the person who created the event
                if (existingEventObject.CreatedBy != eventObject.EditedBy && (JWTClaimsReader.HandleJWTClaims(Values.ClaimRoleName) == RoleEnum.Admin.ToString()))
                {
                    // keeps organized by unchanged
                    eventObject.OrganizedBy = existingEventObject.OrganizedBy;
                }

                // handle image
                if (request.Banner != null)
                {
                    if (startDate > DateTime.Now)
                    {
                        // validate banner size and type
                        var bannerValidator = new ImageValidator();
                        var bannerValidatorResult = bannerValidator.Validate(request.Banner);

                        if (!bannerValidatorResult.IsValid)
                        {
                            return bannerValidatorResult.CreateErrorsResponsesWithBool(HttpStatusCode.BadRequest);
                        }

                        var extension = Path.GetExtension(request.Banner.ImageFile.FileName);
                        var path = Values.PathForEvents + eventId + extension;
                        string imagePath = $"{_blobStorageSettings.AzurePath}{path}";

                        var status = _fileService.Upload(request.Banner, path);
                        if (status.Data == false)
                        {
                            return CustomResponseExtension.ResponseInternalServerError<bool>(Values.InternalServerErrorMessage);
                        }
                        eventObject.Banner = imagePath;
                    }
                    else
                    {
                        return CustomResponseExtension.GenericResponse<bool>(HttpStatusCode.Forbidden, Errors.CannotChangeActiveOrPastEventBanner);
                    }
                }
                else
                {
                    eventObject.Banner = existingEventObject.Banner;
                }

                // handle user invitations for the event
                List<Invitation> existingEventInvitations = _unitOfWork.InvitationRepository.GetAllEventInvites(eventId).ToList();
                List<Invitation> userInvitationsToRemove = new List<Invitation>();
                List<User> usersToRemove = new List<User>();
                List<string> usersAlreadyInvited = new List<string>();
                List<User> usersToAdd = new List<User>();

                // get the ID of a creator to automatically reinvite if needed
                var creatorId = existingEventObject.CreatedBy;

                var requestHasInvitees = request.InviteesIds.Count > 0;
                if (requestHasInvitees || existingEventInvitations.Any())
                {

                    foreach (var invitation in existingEventInvitations)
                    {
                        // check which invitations are missing by comparing request and DB;
                        // then prepare users and ivnitations to for deletion
                        if (!request.InviteesIds.Contains(invitation.UserId))
                        {
                            userInvitationsToRemove.Add(invitation);
                            usersToRemove.Add(invitation.User);
                        }
                        else
                        {
                            usersAlreadyInvited.Add(invitation.UserId);
                        }
                    }

                    // separate users that were invited in this event update
                    usersToAdd = _unitOfWork.UserRepository.GetAll().Where(x => request.InviteesIds.Contains(x.Id) && !usersAlreadyInvited.Contains(x.Id)).ToList();

                    if (userInvitationsToRemove.Any() || usersToAdd.Any() || usersAlreadyInvited.Any())
                    {
                        _unitOfWork.InvitationRepository.RemoveByList(userInvitationsToRemove);

                        // add all invited users to invitation table
                        foreach (User user in usersToAdd)
                        {
                            Invitation invitation = InvitationModelMapper.CreateInvitationModel(user.Id, eventId);
                            _unitOfWork.InvitationRepository.Add(invitation);
                        }

                        // updating the isInviteOnly field depending on whether invitations exist
                        if (!usersToAdd.Any() && ((userInvitationsToRemove.Count() == existingEventInvitations.Count()) || !existingEventInvitations.Any()))
                        {
                            eventObject.IsInviteOnly = false;
                        }
                        else
                        {
                            eventObject.IsInviteOnly = true;

                            // if the creator is not already invited add them to the invitation table
                            if (!usersToAdd.Exists(x => x.Id == creatorId) && !usersAlreadyInvited.Exists(x => x == creatorId))
                            {
                                Invitation creatorInvitation = InvitationModelMapper.CreateInvitationModel(creatorId, eventId);
                                _unitOfWork.InvitationRepository.Add(creatorInvitation);
                            }
                        }
                    }
                }

                // check if the creator has been removed from the invitations list
                var editorId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
                var isCreatorRemoved = usersToRemove.Exists(x => x.Id == creatorId);

                // if yes - check whether the event is being edited by the creator or another Admin
                // to decide whether to send an email notification or not
                if (editorId != creatorId)
                {
                    var creator = _unitOfWork.UserRepository.GetUserById(creatorId);

                    var email = creator.SendEmailToCreatorForUpdateEvent(existingEventObject, eventLink, _emailRepository);
                    _emailService.SendEmail(email);
                }

                // now remove the creator from usersToRemove because of unnecessary email notifications
                if (isCreatorRemoved)
                {
                    usersToRemove.RemoveAll(x => x.Id == creatorId);
                }

                // handle Status
                if (requestUserRole == RoleEnum.Admin.ToString())
                {
                    eventObject.Status = EventStatusEnum.Approved.ToString();
                    SendInvitationEmailToUsers(usersToAdd, existingEventObject, eventLink, _emailRepository);
                }

                if (requestUserRole == RoleEnum.User.ToString())
                {
                    eventObject.Status = EventStatusEnum.Pending.ToString();

                    bool isEventDuringWeekend = IsEventDuringWeekend(existingEventObject);
                    if (isEventDuringWeekend)
                    {
                        eventObject.Status = EventStatusEnum.Approved.ToString();
                        SendInvitationEmailToUsers(usersToAdd, existingEventObject, eventLink, _emailRepository);
                    }
                }

                if (requestHasInvitees)
                {
                    SendUninvitedEmailToUsers(usersToRemove, eventObject, _emailRepository);
                }

                _unitOfWork.EventRepository.Update(eventObject);
                _unitOfWork.Complete();

                var invitations = _unitOfWork.InvitationRepository.GetAllEventInvites(existingEventObject.Id)
                    .Where(inv => inv.InviteStatus == InviteStatusEnum.Going || inv.InviteStatus == InviteStatusEnum.Maybe).ToList();

                if (endDate != request.DateEnd || startDate != request.DateStart)
                {
                    SendEventUpdateEmailToUsers(invitations, existingEventObject, eventLink, _emailRepository);
                }

                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.SuccessfulEdit, true);
            }
            catch (Exception ex)
            {
                return CustomResponseExtension.ResponseInternalServerError<bool>(ex.Message);
            }

        }

        public Response<bool> ApproveEvent(ApproveEventStatusRequest request)
        {
            var eventObject = _unitOfWork.EventRepository.Get(request.Id);
            if (eventObject == null)
            {
                return CustomResponseExtension.ResponseEventNotFound<bool>();
            }

            string eventLink = GetEventUrl(eventObject);

            if (eventObject.Status == EventStatusEnum.Pending.ToString())
            {
                List<Invitation> invitations = _unitOfWork.InvitationRepository.GetAllEventInvites(eventObject.Id).ToList();
                List <User> users = new List<User>();

                foreach(var invitation in invitations)
                {
                    users.Add(invitation.User);
                }

                var updatedStatusEvent = AcceptOrDeclineExtension.HandleUpdateStatus(eventObject, request);
                _unitOfWork.EventRepository.Update(updatedStatusEvent);
                _unitOfWork.Complete();

                SendInvitationEmailToUsers(users, eventObject, eventLink, _emailRepository);

                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.ApprovedEvent, true);
            }
            else
            {
                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.BadRequest, Messages.NotPendingEvent, false);
            }
        }

        public Response<bool> DeclineEvent(DeclineEventStatusRequest request)
        {
            var eventObject = _unitOfWork.EventRepository.Get(request.Id);
            if (eventObject == null)
            {
                return CustomResponseExtension.ResponseEventNotFound<bool>();
            }

            string eventLink = GetEventUrl(eventObject);

            if (eventObject.Status == EventStatusEnum.Pending.ToString())
            {
                var updatedStatusEvent = AcceptOrDeclineExtension.HandleUpdateStatus(eventObject, request);
                _unitOfWork.EventRepository.Update(updatedStatusEvent);
                _unitOfWork.Complete();

                User user = _userRepository.GetUserById(eventObject.CreatedBy);

                var email = user.SendEmailForDeclineEvent(eventObject, eventLink, request.DeclineReason, _emailRepository);
                _emailService.SendEmail(email);

                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.DeclinedEvent, true);
            }
            else
            {
                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.BadRequest, Messages.NotPendingEvent, false);
            }
           
        }

        public Response<SingleEventResponse> CreateEvent(CreateEventRequest request)
        {
            var eventValidator = new EventValidator();
            var eventValidatorResult = eventValidator.Validate(request);
            
            if (!eventValidatorResult.IsValid)
            {
                return eventValidatorResult.CreateErrorResponseWithGenericType<SingleEventResponse>(HttpStatusCode.BadRequest);
            }

            try
            {
                if (request.Type == Values.TypeOnSite)
                {
                    bool isLink = Uri.IsWellFormedUriString(request.LocationUrl, UriKind.Absolute);
                    if (isLink)
                    {
                        return CustomResponseExtension.ResponseBadRequest<SingleEventResponse>(Errors.InvalidLocationForOnSiteEvent);
                    }
                }

                string requestUserRole = JWTClaimsReader.HandleJWTClaims(Values.ClaimRoleName);
                if(requestUserRole == RoleEnum.User.ToString())
                {
                    if(request.OrganizedBy == OrganizedByEnum.Company.ToString())
                    {
                        return CustomResponseExtension.ResponseBadRequest<SingleEventResponse>(Errors.InvalidOrganizatorForUserRole);
                    }
                }

                List<User> usersToInvite = _unitOfWork.UserRepository.GetAll().Where(x => request.InviteesIds.Contains(x.Id)).ToList();
                Event eventObject = request.CreateEventModel();

                string eventLink = GetEventUrl(eventObject);

                if (usersToInvite.Any())
                {
                    foreach (User user in usersToInvite)
                    {
                        Invitation invitation = InvitationModelMapper.CreateInvitationModel(user.Id, eventObject.Id);
                        _unitOfWork.InvitationRepository.Add(invitation);
                    }
                    eventObject.IsInviteOnly = true;

                    // if the creator is not already invited add them to the invitee list and don't notify them via an email
                    string requestUserId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
                    if (!usersToInvite.Exists(x => x.Id == requestUserId))
                    {
                        User user = _unitOfWork.UserRepository.Get(requestUserId);
                        Invitation creatorInvitation = InvitationModelMapper.CreateInvitationModel(user.Id, eventObject.Id);
                        _unitOfWork.InvitationRepository.Add(creatorInvitation);
                    }
                }
                else
                {
                    eventObject.IsInviteOnly = false;
                }

                if (request.Banner != null)
                {
                    // validate banner size and type
                    var bannerValidator = new ImageValidator();
                    var bannerValidatorResult = bannerValidator.Validate(request.Banner);

                    if (!bannerValidatorResult.IsValid)
                    {
                        return bannerValidatorResult.CreateErrorResponseWithGenericType<SingleEventResponse>(HttpStatusCode.BadRequest);
                    }

                    var extension = Path.GetExtension(request.Banner.ImageFile.FileName);
                    var path = Values.PathForEvents + eventObject.Id + extension;
                    string imagePath = $"{_blobStorageSettings.AzurePath}{path}";

                    var status = _fileService.Upload(request.Banner, path);
                    if (status.Data == false)
                    {
                        return CustomResponseExtension.ResponseInternalServerError<SingleEventResponse>(Values.InternalServerErrorMessage);
                    }
                    eventObject.Banner = imagePath;
                }
                else
                {
                    eventObject.Banner = $"{_blobStorageSettings.AzurePath}{Values.DefaultEventBanner}";
                }

                // Handle Status
                if (requestUserRole == RoleEnum.Admin.ToString())
                {
                    eventObject.Status = EventStatusEnum.Approved.ToString();
                    SendInvitationEmailToUsers(usersToInvite, eventObject, eventLink, _emailRepository);
                }

                if (requestUserRole == RoleEnum.User.ToString())
                {
                    eventObject.Status = EventStatusEnum.Pending.ToString();

                    bool isEventDuringWeekend = IsEventDuringWeekend(eventObject);
                    if (isEventDuringWeekend)
                    {
                        eventObject.Status = EventStatusEnum.Approved.ToString();
                        SendInvitationEmailToUsers(usersToInvite, eventObject, eventLink, _emailRepository);
                    }
                }

                // get the userId and append it on createdby
                var userId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);
                eventObject.CreatedBy = userId;

                _unitOfWork.EventRepository.Add(eventObject);
                _unitOfWork.Complete();

                var response = EventExpression.MapToEventDto().Compile()(eventObject);
                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.Created, Messages.SuccessfulSubmit, response);
            }
            catch (Exception ex)
            {
                return CustomResponseExtension.ResponseInternalServerError<SingleEventResponse>(ex.Message);
            }
        }

        public Response<IEnumerable<UserCalendarEventResponse>> GetAllEventsForUserCalendar()
        {
            var userId = JWTClaimsReader.HandleJWTClaims(Values.ClaimId);

            var events = _unitOfWork.EventRepository.GetEventsForUser(userId);
            events = events.Where(ev => ev.DeletedAt == null);
            
            var response = events.Select(MapperExpression<Event, UserCalendarEventResponse>.CreateMapExpression().Compile());

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }

        #region Helper Classes

        private string GetEventUrl(Event eventObject)
        {
            return new string($"{_urls.UiUrl}events/{eventObject.Id}");
        }

        private void SendInvitationEmailToUsers(List<User> users, Event eventObject, string eventLink, IEmailRepository emailRepository)
        {
            foreach (var user in users)
            {
                var email = user.SendEmailToInvitees(eventObject, eventLink, emailRepository);
                _emailService.SendEmail(email);
            }
        }

        private void SendUninvitedEmailToUsers(List<User> users, Event eventObject, IEmailRepository emailRepository)
        {
            foreach (var user in users)
            {
                var email = user.SendEmailToDeletedInvitees(eventObject, emailRepository);
                _emailService.SendEmail(email);
            }
        }

        private void SendEventUpdateEmailToUsers(List<Invitation> invitations, Event eventObject, string eventLink, IEmailRepository emailRepository)
        {
            foreach (var invitation in invitations)
            {
                var email = invitation.SendEmailToInviteesForUpdateEvent(eventObject, eventLink, emailRepository);
                _emailService.SendEmail(email);
            }
        }

        private void SendCancelEventEmailToUsers(List<Invitation> invitations, Event eventObject, string eventOrganizer, IEmailRepository emailRepository)
        {
            foreach (var invitation in invitations)
            {
                var email = invitation.SendEmailForCancelEvent(eventObject, eventOrganizer, emailRepository);
                _emailService.SendEmail(email);
            }
        }

        private bool IsEventDuringWeekend(Event eventObject)
        {
            // event starts on saturday and ends during the same weekend (satruday or sunday)
            bool isSaturdayOrWeekendEvent = eventObject.DateStart.DayOfWeek == DayOfWeek.Saturday && eventObject.DateEnd.Date <= eventObject.DateStart.Date.AddDays(1);

            // event starts on sunday and ends same day
            bool isSundayEvent = eventObject.DateStart.DayOfWeek == DayOfWeek.Sunday && eventObject.DateEnd.Date == eventObject.DateStart.Date;

            return isSaturdayOrWeekendEvent || isSundayEvent;
        }

        #endregion
    }
}
