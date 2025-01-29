using Application.Ads.Dtos;
using AutoMapper;

namespace Application.Ads.Mappings
{
    internal class AdsMappings : Profile
    {
        public AdsMappings()
        {

            CreateMap<CreateAdDto, Ad>();
            CreateMap<UpdateAdDto, Ad>();
            CreateMap<Ad, AdDto>();

            CreateMap<CreateAdOfferDto, AdOffer>();
            CreateMap<AdOffer, FacilityAdOfferDto>();
            CreateMap<AdOffer, CompanyAdOfferDto>()
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.Ad.Facility));
        }
    }
}
