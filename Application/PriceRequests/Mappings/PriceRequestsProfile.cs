﻿using Application.PriceRequests.Dtos;
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
            CreateMap<PriceRequest, GetPriceRequestBriefDto>().ReverseMap();

        }
    }
}
