using GatherApp.Contracts.Entities;
using System.Runtime.CompilerServices;

namespace GatherApp.Services.Extensions
{
    static class InvitationModelMapper
    {
        public static Invitation CreateInvitationModel(string userId, string eventId)
        {
            return new Invitation
            {
                InviteStatus = Contracts.Enums.InviteStatusEnum.NoResponse,
                EventId = eventId,
                UserId = userId,

            };
        }
    }
}
