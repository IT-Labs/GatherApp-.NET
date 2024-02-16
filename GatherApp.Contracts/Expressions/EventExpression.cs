using System;
using System.Linq.Expressions;
using GatherApp.Contracts.Entities;
using AutoMapper;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Enums;

namespace GatherApp.Contracts.Expressions
{
    public class EventExpression
    {
        private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Event, SingleEventResponse>();
            cfg.CreateMap<User, UserBasicDto>().ForMember(user => user.FullName,
             opt => opt.MapFrom(source => source.FirstName + ' ' + source.LastName));
            cfg.CreateMap<Invitation, EventInvitationDto>().ForMember(inv => inv.InviteStatus,
             opt => opt.MapFrom(source => Enum.GetName(typeof(InviteStatusEnum), source.InviteStatus)));

        })
        .CreateMapper();
        private static readonly IMapper _mapperEvents = new MapperConfiguration(cfg => cfg.CreateMap<Event, UserCalendarEventResponse>()).CreateMapper();

        public static Expression<Func<Event, SingleEventResponse>> MapToEventDto()
        {
            return eventObj => _mapper.Map<SingleEventResponse>(eventObj);
        }

        public static Expression<Func<Event, UserCalendarEventResponse>> MapToMyEventDto()
        {
            return eventObj => _mapperEvents.Map<UserCalendarEventResponse>(eventObj);

        }

        public static Expression<Func<Invitation, EventInvitationDto>> MapToEventInvitationDto()
        {
            return invitationObj => _mapper.Map<EventInvitationDto>(invitationObj);
        }
    }
}
