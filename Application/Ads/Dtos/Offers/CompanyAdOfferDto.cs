using Application.Ads.Dtos.AdServices;
using Application.Facilities.Dtos;

namespace Application.Ads.Dtos.Offers
{
    public class CompanyAdOfferDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }

        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public ICollection<AdServicePriceDto> ServicesPrice { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}


