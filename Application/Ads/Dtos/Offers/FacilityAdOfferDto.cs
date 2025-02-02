using Application.Ads.Dtos.AdServices;
using Application.Companies.Dtos;

namespace Application.Ads.Dtos.Offers
{
    public class FacilityAdOfferDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public ICollection<AdServicePriceDto> ServicesPrice { get; set; }

        public CompanyBreifDto Company { get; set; }
    }
}