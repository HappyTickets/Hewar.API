using Application.Ads.Dtos;
using Application.Ads.Dtos.AdServices;
using Application.Ads.Dtos.Offers;
using Application.Ads.Dtos.Post;
using AutoMapper;
using Domain.Entities.AdAggregate;

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


            CreateMap<AdService, AdServiceDto>().ReverseMap();

            CreateMap<AdServicePrice, AdServicePriceDto>().ReverseMap();

        }
    }
}
