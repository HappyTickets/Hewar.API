using Application.Ads.Dtos.AdServices.Res;
using Application.Facilities.Dtos;

namespace Application.Ads.Dtos.Offers
{
    public class CompanyAdOfferDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }

        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public ICollection<GetAdHewarServiceCostDto> ServicesCost { get; set; }
        public ICollection<GetOtherAdServiceCostDto>? OtherServicesCost { get; set; }
        public ICollection<GetAdCompanyServiceCostDto>? CompanyServicesCost { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}


