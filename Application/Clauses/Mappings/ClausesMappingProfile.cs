using Application.Clauses.DTOs;
using AutoMapper;
using Domain.Entities.ContractAggregate.Dynamic;
using Domain.Entities.ContractAggregate.Static;

namespace Application.Clauses.Mappings
{
    internal class ClausesMappingProfile : Profile
    {
        public ClausesMappingProfile()
        {
            CreateMap<StaticClause, StaticClauseDto>()
                .ReverseMap();

            CreateMap<CustomClause, CustomClauseDto>()
                .ReverseMap();
        }
    }
}
