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

            CreateMap<ServiceRequestDto, ServiceRequest>().ReverseMap();

            CreateMap<OtherRequestedService, CreateOtherServiceDto>().ReverseMap();

            CreateMap<OtherRequestedService, OtherRequestedServiceDto>().ReverseMap();


            CreateMap<PriceRequest, FacilityPriceRequestDto>()
                .ForMember(dest => dest.HasOffers, opt => opt.MapFrom(src => src.Offers != null && src.Offers.Any()))
                .ReverseMap();

            CreateMap<PriceRequest, CompanyPriceRequestDto>()
                .ForMember(dest => dest.HasOffers, opt => opt.MapFrom(src => src.Offers != null && src.Offers.Any()))
                .ReverseMap();

            CreateMap<PriceRequest, GetPriceRequestBriefDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RequestStatus))
                .ReverseMap();

            CreateMap<PriceRequest, GetPriceRequestDto>()
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
                .ForMember(dest => dest.FacilityId, opt => opt.MapFrom(src => src.Facility.Id))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RequestStatus))
                .ForMember(dest => dest.HasOffers, opt => opt.MapFrom(src => src.Offers != null && src.Offers.Any()))
                .ReverseMap();

        }
    }
}
