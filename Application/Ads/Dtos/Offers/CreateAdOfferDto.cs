using Application.Ads.Dtos.AdServices;

namespace Application.Ads.Dtos.Offers
{
    public class CreateAdOfferDto
    {
        public long AdId { get; set; }
        public ICollection<AdServicePriceDto> ServicesPrice { get; set; }
    }
}
