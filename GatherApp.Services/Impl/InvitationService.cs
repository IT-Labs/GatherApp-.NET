using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Repositories;
using System.Net;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Enums;
using GatherApp.Contracts.Dtos;
using GatherApp.Services.Extensions;
using GatherApp.Contracts.Entities;

namespace GatherApp.Services.Impl
{
    public class InvitationService : IInvitationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvitationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<string> RespondToEventInvitation(UpdateInviteStatusRequest request)
        {
            var eventObject = _unitOfWork.EventRepository.Get(request.EventId);
            if (eventObject == null)
            {
                return CustomResponseExtension.ResponseEventNotFound<string>();
            }

            var user = _unitOfWork.UserRepository.Get(request.UserId);
            if (user == null)
            {
                return CustomResponseExtension.ResponseUserNotFound<string>();
            }

            var invitation = _unitOfWork.InvitationRepository.Get(request.InvitationId);
            if (invitation != null)
            {
                var updateInvitationResponse = RespondToEventInvitationExtension.HandleUpdateInviteStatus(invitation, request);

                _unitOfWork.InvitationRepository.Update(updateInvitationResponse);
                _unitOfWork.Complete();

                return ResponseAfterInvitationStatusUpdate(updateInvitationResponse);
            }

            return RespondToPublicEvent(eventObject, user, request);
        }

        private Response<string> RespondToPublicEvent(Event eventObject, User user, UpdateInviteStatusRequest request)
        {
            InvitationDto newInvitationStatus = eventObject.ToInvitationDto(user, request.InviteStatus);

            _unitOfWork.InvitationRepository.Add(newInvitationStatus);
            _unitOfWork.Complete();

            var updateInvitationResponse = RespondToEventInvitationExtension.HandleUpdateInviteStatus(newInvitationStatus, request);

            _unitOfWork.InvitationRepository.Update(updateInvitationResponse);
            _unitOfWork.Complete();

            return ResponseAfterInvitationStatusUpdate(updateInvitationResponse);
        }

        private Response<string> ResponseAfterInvitationStatusUpdate(Invitation invitation)
        {
            if (invitation.InviteStatus == InviteStatusEnum.Going)
                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.UserGoing, invitation.Id);

            if (invitation.InviteStatus == InviteStatusEnum.NotGoing)
                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.UserNotGoing, invitation.Id);

            if (invitation.InviteStatus == InviteStatusEnum.Maybe)
                return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, Messages.UserMaybeGoing, invitation.Id);

            return CustomResponseExtension.ResponseInternalServerError<string>(Errors.UnexpectedError);
        }
    }
}
