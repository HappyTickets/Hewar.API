using Application.PriceOffers.Dtos.Services;

namespace Application.PriceOffers.Dtos
{
    public record UpdatePriceOfferDto
    {
        public long PriceOfferId { get; set; }
        public ICollection<CreateServiceOfferDto> Services { get; set; }
        public ICollection<CreateOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
