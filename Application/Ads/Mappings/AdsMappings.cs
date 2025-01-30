using Application.Ads.Dtos;
using AutoMapper;

namespace Application.Ads.Mappings
{
    internal class AdsMappings : Profile
    {
        public AdsMappings()
        {

            CreateMap<CreateAdDto, Ad>();
            CreateMap<UpdateAdDto, Ad>();
            CreateMap<Ad, AdDto>();

            CreateMap<CreateAdOfferDto, AdOffer>();
            CreateMap<AdOffer, FacilityAdOfferDto>();
            CreateMap<AdOffer, CompanyAdOfferDto>()
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.Ad.Facility));

            //CreateMap<AdOffer, AdOfferMessageDto>()
            //    .ForMember(s=>s.Content,des=>des.MapFrom(des=>des.Chat.Content))
            //    .ForMember(s=>s.OfferId,des=>des.MapFrom(des=>des.Id))
            //    .ForMember(s=>s.Medias,des=>des.MapFrom(des=>des.Chat.Messages.))
            //    .ForMember(s=>s.OfferId,des=>des.MapFrom(des=>des.Id))
            //    .ForMember(s=>s.OfferId,des=>des.MapFrom(des=>des.Id))
        }
    }
}
