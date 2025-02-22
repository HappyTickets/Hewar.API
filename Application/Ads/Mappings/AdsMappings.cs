using Application.Ads.Dtos;
using Application.Ads.Dtos.AdServices.Req;
using Application.Ads.Dtos.AdServices.Res;
using Application.Ads.Dtos.Offers;
using Application.Ads.Dtos.Post;
using AutoMapper;
using Domain.Entities.AdAggregate.Services;

namespace Application.Ads.Mappings
{
    internal class AdsMappings : Profile
    {
        public AdsMappings()
        {

            CreateMap<CreateAdDto, Ad>();
            CreateMap<UpdateAdOfferDto, Ad>();

            CreateMap<Ad, AdDto>();

            CreateMap<CreateAdOfferDto, AdOffer>();
            CreateMap<UpdateAdOfferDto, AdOffer>();

            CreateMap<AdOffer, FacilityAdOfferDto>();

            CreateMap<AdOffer, CompanyAdOfferDto>()
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.Ad.Facility));


            CreateMap<AdHewarService, SelectHewarServiceDto>().ReverseMap();

            CreateMap<AdHewarService, GetAdHewarServiceDto>().ReverseMap();
            CreateMap<AdHewarServiceCost, CreateAdHewarServiceCostDto>().ReverseMap();
            CreateMap<AdHewarServiceCost, GetAdHewarServiceCostDto>().ReverseMap();


            CreateMap<OtherAdService, CreateOtherAdServiceDto>().ReverseMap();
            CreateMap<OtherAdService, GetOtherAdServiceDto>().ReverseMap();

            CreateMap<OtherAdServiceCost, CreateOtherAdServiceCostDto>().ReverseMap();
            CreateMap<OtherAdServiceCost, GetOtherAdServiceCostDto>().ReverseMap();

            CreateMap<AdCompanyServiceCost, CreateAdCompanyServiceCostDto>().ReverseMap();
            CreateMap<AdCompanyServiceCost, GetAdCompanyServiceCostDto>().ReverseMap();
        }
    }
}
