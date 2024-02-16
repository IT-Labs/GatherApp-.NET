using System;
using System.Linq.Expressions;
using GatherApp.Contracts.Entities;
using AutoMapper;

namespace GatherApp.Contracts.Expressions
{
    public static class MapperExpression<TSource, TDestination>
    {
        private static readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();

        public static Expression<Func<TSource, TDestination>> CreateMapExpression()
        {
            return sourceObj => _mapper.Map<TDestination>(sourceObj);
        }
    }
}
