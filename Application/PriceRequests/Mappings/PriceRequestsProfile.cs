using Application.Companies.Dtos;
using Application.Facilities.Dtos;
using Application.PriceRequests.Dtos;
using AutoMapper;
using Domain.Entities.PriceRequestAggregates;

namespace Application.PriceRequests.Mappings
{
    internal class PriceRequestsProfile: Profile
    {
        public PriceRequestsProfile()
        {
            CreateMap<CreatePriceRequestDto, PriceRequest>();
            CreateMap<PriceRequest, FacilityPriceRequestDto>();
            CreateMap<PriceRequest, CompanyPriceRequestDto>();

            CreateMap<CreatePriceRequestOfferDto, PriceRequestOffer>();
            CreateMap<PriceRequestOffer, PriceRequestOfferDto>();
           
            CreateMap<CreatePriceRequestFacilityDetailsDto, PriceRequestFacilityDetails>();
            CreateMap<UpdatePriceRequestFacilityDetailsDto, PriceRequestFacilityDetails>();
            CreateMap<PriceRequestFacilityDetails, PriceRequestFacilityDetailsDto>();
        }
    }
}
