using Application.PriceOffers.Dtos.Services;
using Application.PriceRequests.Dtos;

namespace Application.PriceOffers.Dtos
{
    public class GetPriceOfferDetailedDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }

        public GetPriceRequestBriefDto PriceRequest { get; set; }
        public string FacilityName { get; set; }
        public RequestStatus OfferStatus { get; set; }
        public ICollection<ServiceOfferDto> Services { get; set; }
        public ICollection<GetOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
