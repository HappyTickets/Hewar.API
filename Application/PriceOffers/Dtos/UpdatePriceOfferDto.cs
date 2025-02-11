using Application.PriceOffers.Dtos.Services;

namespace Application.PriceOffers.Dtos
{
    public record UpdatePriceOfferDto
    {
        public long PriceOfferId { get; set; }

        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public ICollection<CreateServiceOfferDto> Services { get; set; }
        public ICollection<CreateOtherServiceOfferDto>? OtherServices { get; set; }
    }
}
