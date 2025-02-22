using Application.Ads.Dtos.AdServices.Res;
using Application.Companies.Dtos;

namespace Application.Ads.Dtos.Offers
{
    public class FacilityAdOfferDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }

        public ICollection<GetAdHewarServiceCostDto> ServicesCost { get; set; }
        public ICollection<GetOtherAdServiceCostDto>? OtherServicesCost { get; set; }
        public ICollection<GetAdCompanyServiceCostDto>? CompanyServicesCost { get; set; }

        public CompanyBreifDto Company { get; set; }
    }
}