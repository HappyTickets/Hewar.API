using Application.InsuranceAds.Dtos;
using AutoMapper;

namespace Application.InsuranceAds.Mappings
{
    internal class InsuranceAdsMappings : Profile
    {
        public InsuranceAdsMappings()
        {

            CreateMap<CreateInsuranceAdDto, Ad>();
            CreateMap<UpdateInsuranceAdDto, Ad>();
            CreateMap<Ad, InsuranceAdDto>();

            CreateMap<CreateInsuranceAdOfferDto, AdOffer>();
            CreateMap<AdOffer, FacilityInsuranceAdOfferDto>();
            CreateMap<AdOffer, CompanyInsuranceAdOfferDto>()
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.Ad.Facility));
        }
    }
}
