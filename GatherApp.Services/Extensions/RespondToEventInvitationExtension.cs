using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Requests;

namespace GatherApp.Services.Extensions
{
    public class RespondToEventInvitationExtension
    {
        public static Invitation HandleUpdateInviteStatus(Invitation response, UpdateInviteStatusRequest request)
        {
            response.InviteStatus = request.InviteStatus;   

            return response;
        }
    }
}
