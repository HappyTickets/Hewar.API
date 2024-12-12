using Application.Files.Dtos;
using AutoMapper;


namespace Application.Files.Mappings
{
    internal class MediaProfile: Profile
    {
        public MediaProfile()
        {
            CreateMap<Media, MediaDto>();
        }
    }
}
