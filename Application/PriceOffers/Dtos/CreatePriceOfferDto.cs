using Application.PriceOffers.Dtos.Services;

namespace Application.PriceOffers.Dtos
{
    public class CreatePriceOfferDto
    {
        public long PriceRequestId { get; set; }
        public ICollection<CreateServiceOfferDto> Services { get; set; }
        public ICollection<CreateOtherServiceOfferDto>? OtherServices { get; set; }
    }
}