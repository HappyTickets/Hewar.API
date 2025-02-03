using Application.Hewar.Dtos;
using AutoMapper;
using Domain.Entities.Hewar;

namespace Application.Hewar.Mappings
{
    internal class HewarProfile : Profile
    {
        public HewarProfile()
        {
            CreateMap<CreateHewarServiceDto, HewarService>();

            CreateMap<HewarService, HewarServiceDto>();
        }
    }
}
