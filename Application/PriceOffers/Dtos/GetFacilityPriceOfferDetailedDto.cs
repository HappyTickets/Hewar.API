using Application.PriceOffers.Dtos.Services;
using Application.PriceRequests.Dtos;

namespace Application.PriceOffers.Dtos
{
    public class GetFacilityPriceOfferDetailedDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }

        public GetPriceRequestBriefDto PriceRequest { get; set; }

        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public RequestStatus OfferStatus { get; set; }
        public ICollection<GetServiceCostDto> Services { get; set; }
        public ICollection<GetOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
