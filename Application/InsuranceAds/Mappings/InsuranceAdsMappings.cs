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


            CreateMap<InsuranceAdOffer, FacilityInsuranceAdOfferDto>();
            CreateMap<InsuranceAdOffer, CompanyInsuranceAdOfferDto>();


            CreateMap<InsuranceAdOfferMessage, InsuranceAdOfferMessageDto>();
        }
    }
}
