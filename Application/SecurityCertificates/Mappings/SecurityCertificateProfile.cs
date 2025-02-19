using Application.SecurityCertificates.DTOs;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.SecurityCertificates.Mappings
{
    public class ScheduleEntryProfile : Profile
    {
        public ScheduleEntryProfile()
        {
            CreateMap<SecurityCertificateCreateDto, SecurityCertificate>();

            CreateMap<SecurityCertificateUpdateDto, SecurityCertificate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<SecurityCertificate, SecurityCertificateDto>()
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name));

            CreateMap<SecurityCertificateStatusChangeDto, SecurityCertificate>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
