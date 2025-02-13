using Application.PriceRequests.Dtos;
using Application.PriceRequests.Dtos.Services;
using AutoMapper;

namespace Application.PriceRequests.Mappings
{
    internal class PriceRequestsProfile : Profile
    {
        public PriceRequestsProfile()
        {
            CreateMap<CreatePriceRequestDto, PriceRequest>();
            CreateMap<UpdatePriceRequestDto, PriceRequest>();

            CreateMap<CreateServiceRequestDto, ServiceRequest>().ReverseMap();
            CreateMap<GetServiceRequestDto, ServiceRequest>().ReverseMap();

            CreateMap<OtherRequestedService, CreateOtherServiceDto>().ReverseMap();

            CreateMap<OtherRequestedService, GetOtherRequestedServiceDto>().ReverseMap();


            CreateMap<PriceRequest, FacilityPriceRequestDto>()
                .ForMember(dest => dest.HasOffers, opt => opt.MapFrom(src => src.Offers != null && src.Offers.Any()))
                .ReverseMap();

            CreateMap<PriceRequest, CompanyPriceRequestDto>()
                .ForMember(dest => dest.HasOffers, opt => opt.MapFrom(src => src.Offers != null && src.Offers.Any()))
                .ReverseMap();

            CreateMap<PriceRequest, GetPriceRequestBriefDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RequestStatus))
                .ReverseMap();

            CreateMap<PriceRequest, GetPriceRequestDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RequestStatus))
                .ForMember(dest => dest.HasOffers, opt => opt.MapFrom(src => src.Offers != null && src.Offers.Any()))
                .ReverseMap();

        }
    }
}
