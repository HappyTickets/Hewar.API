using Application.Chats.DTOs;
using Application.PriceRequests.Dtos.Offers;
using Application.PriceRequests.Dtos.Offers.Services;
using Application.PriceRequests.Dtos.Requests;
using Application.PriceRequests.Dtos.Requests.Services;
using AutoMapper;
using Domain.Entities.ChatAggregate;

namespace Application.PriceRequests.Mappings
{
    internal class PriceRequestsProfile : Profile
    {
        public PriceRequestsProfile()
        {
            CreateMap<CreatePriceRequestDto, PriceRequest>();

            CreateMap<ServiceRequestDto, ServiceRequest>().ReverseMap();

            CreateMap<OtherRequestedService, CreateOtherRequestedServiceDto>()
                .ReverseMap();

            CreateMap<OtherRequestedService, OtherRequestedServiceDto>()
                .ReverseMap();


            CreateMap<PriceRequest, FacilityPriceRequestDto>()
                .ReverseMap();

            CreateMap<PriceRequest, CompanyPriceRequestDto>().ReverseMap();

            CreateMap<ServiceOfferDto, ServiceOffer>().ReverseMap();

            CreateMap<GetOtherServiceOfferDto, OtherServiceOffer>().ReverseMap();
            CreateMap<CreateOtherServiceOfferDto, OtherServiceOffer>().ReverseMap();

            CreateMap<Message, ChatMessageDto>().ReverseMap();
            CreateMap<ApplicationUser, ChatParticipantDto>().ReverseMap();

            CreateMap<GetPriceOfferDto, PriceOffer>()
                .ReverseMap();

            CreateMap<CreatePriceOfferDto, PriceOffer>().ReverseMap();
        }
    }
}
