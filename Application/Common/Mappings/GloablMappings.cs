using Application.Common.Utilities;
using AutoMapper;

namespace Application.Common.Mappings
{
    internal class GloablMappings: Profile
    {
        public GloablMappings()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));
        }
    }
}
