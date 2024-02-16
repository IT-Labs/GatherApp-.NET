using AutoMapper;
using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;
using System.Linq.Expressions;

namespace GatherApp.Contracts.Expressions
{
    public class UserExpression
    {
        private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, SingleUserResponse>()
                .ForMember(user => user.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(user => user.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
        })
        .CreateMapper();

        public static Expression<Func<User, SingleUserResponse>> MapToUserDto()
        {
            return userObj => _mapper.Map<SingleUserResponse>(userObj);
        }
    }
}
