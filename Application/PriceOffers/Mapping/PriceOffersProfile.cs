using Application.PriceOffers.Dtos;
using Application.PriceOffers.Dtos.Services;
using AutoMapper;

namespace Application.PriceOffers.Mapping
{
    internal class PriceOffersProfile : Profile
    {
        public PriceOffersProfile()
        {
            CreateMap<CreateServiceOfferDto, ServiceOffer>().ReverseMap();
            CreateMap<GetServiceOfferDto, ServiceOffer>().ReverseMap();

            CreateMap<GetOtherServiceOfferDto, OtherServiceOffer>().ReverseMap();
            CreateMap<CreateOtherServiceOfferDto, OtherServiceOffer>().ReverseMap();


            CreateMap<PriceRequest, GetOffersForRequest>()
                .ForMember(dest => dest.PriceRequest, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.Facility))
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers))
                .ReverseMap();

            CreateMap<PriceOffer, GetPriceOfferDto>().ReverseMap();

            CreateMap<PriceOffer, GetCompanyPriceOfferDetailedDto>()
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.PriceRequest.Facility))
                .ReverseMap();
            CreateMap<PriceOffer, GetFacilityPriceOfferDetailedDto>()
                .ReverseMap();

            CreateMap<CreatePriceOfferDto, PriceOffer>().ReverseMap();
            CreateMap<UpdatePriceOfferDto, PriceOffer>().ReverseMap();
        }
    }
}
