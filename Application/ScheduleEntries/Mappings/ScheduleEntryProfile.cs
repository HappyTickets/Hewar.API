using Application.ScheduleEntries.DTOs;
using AutoMapper;
using Domain.Entities.ContractAggregate.Dynamic;

namespace Application.ScheduleEntries.Mappings
{
    public class ScheduleEntryProfile : Profile
    {
        public ScheduleEntryProfile()
        {
            CreateMap<ScheduleEntryDto, ScheduleEntry>()
                .ReverseMap();

        }
    }
}
