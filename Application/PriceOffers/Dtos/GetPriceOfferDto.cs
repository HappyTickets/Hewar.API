using Application.PriceOffers.Dtos.Services;

namespace Application.PriceOffers.Dtos
{
    public class GetPriceOfferDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public long PriceRequestId { get; set; }

        public ContractType ContractType { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public RequestStatus OfferStatus { get; set; }
        public ICollection<GetServiceOfferDto> Services { get; set; }
        public ICollection<GetOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
