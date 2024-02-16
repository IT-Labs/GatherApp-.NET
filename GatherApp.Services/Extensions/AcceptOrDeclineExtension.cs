using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Requests;

namespace GatherApp.Services.Extensions
{
    public class AcceptOrDeclineExtension
    {
        public static Event HandleUpdateStatus<T>(Event response, T request) where T : IUpdateEventStatusRequest
        { 
            response.Status = request.Status;
            response.EditedAt = DateTime.UtcNow;

            return response;
        }
    }
}
