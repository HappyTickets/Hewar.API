using Application.PriceOffers.Dtos;
using Application.PriceOffers.Dtos.Services;
using AutoMapper;

namespace Application.PriceOffers.Mapping
{
    internal class PriceOffersProfile : Profile
    {
        public PriceOffersProfile()
        {
            CreateMap<ServiceOfferDto, ServiceOffer>().ReverseMap();
            CreateMap<GetOtherServiceOfferDto, OtherServiceOffer>().ReverseMap();
            CreateMap<CreateOtherServiceOfferDto, OtherServiceOffer>().ReverseMap();


            CreateMap<PriceRequest, GetOffersForRequest>()
                .ForMember(dest => dest.PriceRequest, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers))
                .ReverseMap();


            CreateMap<GetPriceOfferDto, PriceOffer>().ReverseMap();
            CreateMap<GetPriceOfferDetailedDto, PriceOffer>().ReverseMap();

            CreateMap<CreatePriceOfferDto, PriceOffer>().ReverseMap();
            CreateMap<UpdatePriceOfferDto, PriceOffer>().ReverseMap();
        }
    }
}
