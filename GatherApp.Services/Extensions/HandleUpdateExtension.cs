using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Requests;

namespace GatherApp.Services.Extensions
{
    public class HandleUpdateExtension
    {
        public static Event HandleUpdate(Event response, UpdateEventRequest request)
        {

            response.Title = request.Title;
            response.Description = request.Description;
            response.DateStart = request.DateStart;
            response.DateEnd = request.DateEnd;
            response.Type = request.Type;
            response.LocationUrl = request.LocationUrl;
            response.EditedAt = DateTime.UtcNow;
            response.OrganizedBy = request.OrganizedBy;
            response.Category = request.Category;

            return response;
        }
    }
}
