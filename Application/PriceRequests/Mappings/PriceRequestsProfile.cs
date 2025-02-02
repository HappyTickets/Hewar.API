using Application.Chats.DTOs;
using Application.PriceRequests.Dtos.Offers;
using Application.PriceRequests.Dtos.Requests;
using AutoMapper;
using Domain.Entities.ChatAggregate;

namespace Application.PriceRequests.Mappings
{
    internal class PriceRequestsProfile : Profile
    {
        public PriceRequestsProfile()
        {
            CreateMap<CreatePriceRequestDto, PriceRequest>();
            CreateMap<PriceRequestServiceDto, PriceRequestService>();

            CreateMap<PriceRequest, FacilityPriceRequestDto>();
            CreateMap<PriceRequest, CompanyPriceRequestDto>();

            CreateMap<PriceOfferServiceDto, PriceOfferService>().ReverseMap();

            CreateMap<Message, ChatMessageDto>().ReverseMap();
            CreateMap<ApplicationUser, ChatParticipantDto>().ReverseMap();

            CreateMap<PriceOfferDto, PriceOffer>()
                .ForMember(dto => dto.Services, x => x.MapFrom(src => src.PricedServices)).ReverseMap()
                .ReverseMap();

            CreateMap<CreatePriceOfferDto, PriceOffer>()
                .ForMember(dto => dto.Services, x => x.MapFrom(src => src.PricedServices)).ReverseMap();
        }
    }
}
