using Application.Common.Dtos;
using Application.Common.Utilities.Pagination;
using AutoMapper;

namespace Application.Common.Mappings
{
    internal class GloablMappings : Profile
    {
        public GloablMappings()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));
            CreateMap<Media, MediaDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
