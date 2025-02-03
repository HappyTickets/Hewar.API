using Application.PriceRequests.Dtos.Offers.Services;

namespace Application.PriceRequests.Dtos.Offers
{
    public class GetPriceOfferDto
    {
        public long Id { get; set; }
        public long PriceRequestId { get; set; }

        public ICollection<ServiceOfferDto> Services { get; set; }
        public ICollection<GetOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
