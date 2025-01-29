using Application.PriceRequests.Dtos;
using AutoMapper;

namespace Application.PriceRequests.Mappings
{
    internal class PriceRequestsProfile : Profile
    {
        public PriceRequestsProfile()
        {
            CreateMap<CreatePriceRequestDto, PriceRequest>();
            CreateMap<PriceRequest, FacilityPriceRequestDto>();
            CreateMap<PriceRequest, CompanyPriceRequestDto>();

            CreateMap<CreatePriceRequestOfferDto, PriceOffer>();
            CreateMap<PriceOffer, PriceRequestOfferDto>();

        }
    }
}
