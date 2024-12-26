using Application.InsuranceAds.Dtos;
using AutoMapper;

namespace Application.InsuranceAds.Mappings
{
    internal class InsuranceAdsMappings: Profile
    {
        public InsuranceAdsMappings()
        {

            CreateMap<CreateInsuranceAdDto, InsuranceAd>();
            CreateMap<UpdateInsuranceAdDto, InsuranceAd>();
            CreateMap<InsuranceAd, InsuranceAdDto>();

            CreateMap<CreateInsuranceAdOfferDto, InsuranceAdOffer>();   
            CreateMap<InsuranceAdOffer, FacilityInsuranceAdOfferDto>();
            CreateMap<InsuranceAdOffer, CompanyInsuranceAdOfferDto>()
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.InsuranceAd.Facility));

            CreateMap<CreateInsuranceAdOfferMessageDto, InsuranceAdOfferMessage>();
            CreateMap<InsuranceAdOfferMessage, InsuranceAdOfferMessageDto>();
        }
    }
}
