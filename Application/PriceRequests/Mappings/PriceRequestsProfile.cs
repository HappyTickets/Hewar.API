using Application.PriceRequests.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;

namespace Application.PriceRequests.Mappings
{
    internal class PriceRequestsProfile: Profile
    {
        public PriceRequestsProfile()
        {
            CreateMap<CreatePriceRequestDto, PriceRequest>();
            CreateMap<PriceRequest, FacilityPriceRequestDto>();
            CreateMap<PriceRequest, CompanyPriceRequestDto>();

            CreateMap<CreatePriceRequestResponseDto, PriceRequestResponse>();
            CreateMap<PriceRequestResponse, PriceRequestResponseDto>();
           
            CreateMap<CreatePriceRequestFacilityDetailsDto, PriceRequestFacilityDetails>();
            CreateMap<UpdatePriceRequestFacilityDetailsDto, PriceRequestFacilityDetails>();
            CreateMap<PriceRequestFacilityDetails, PriceRequestFacilityDetailsDto>();

            CreateMap<Company, CompanyBreifDto>()
                .IncludeMembers(c => c.LoginDetails);
            CreateMap<ApplicationUser, CompanyBreifDto>();

            CreateMap<Facility, FacilityBreifDto>()
                .IncludeMembers(f => f.LoginDetails);
            CreateMap<ApplicationUser, FacilityBreifDto>();
        }
    }
}
