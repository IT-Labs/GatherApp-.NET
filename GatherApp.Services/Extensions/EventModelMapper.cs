using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Requests;

namespace GatherApp.Services.Extensions
{
    static class EventModelMapper
    {
        public static Event CreateEventModel(this CreateEventRequest entity)
        {
            return new Event
            {
                Title = entity.Title,
                Description = entity.Description,
                DateStart = entity.DateStart,
                DateEnd = entity.DateEnd,
                Type = entity.Type,
                LocationUrl = entity.LocationUrl,
                OrganizedBy = entity.OrganizedBy,
                Category = entity.Category,
        };
        }
    }
}