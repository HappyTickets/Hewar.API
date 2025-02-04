using Application.PriceOffers.Dtos.Services;

namespace Application.PriceOffers.Dtos
{
    public class GetPriceOfferDto
    {
        public long Id { get; set; }
        public long PriceRequestId { get; set; }
        public RequestStatus OfferStatus { get; set; }
        public ICollection<ServiceOfferDto> Services { get; set; }
        public ICollection<GetOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
