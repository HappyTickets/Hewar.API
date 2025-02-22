using Application.Ads.Dtos.AdServices.Req;

namespace Application.Ads.Dtos.Offers
{
    public class CreateAdOfferDto
    {
        public long AdId { get; set; }
        public ICollection<CreateAdHewarServiceCostDto> ServicesCost { get; set; }
        public ICollection<CreateOtherAdServiceCostDto>? OtherServicesCost { get; set; }
        public ICollection<CreateAdCompanyServiceCostDto>? CompanyServicesCost { get; set; }

    }
}
