using Application.Companies.Dtos;
using Application.Facilities.Dtos;

namespace Application.InsuranceAds.Dtos
{
    public class CompanyInsuranceAdOfferDto
    {
        public long Id { get; set; }
        public string Offer { get; set; }
        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long InsuranceAdId { get; set; }

        public FacilityBreifDto Facility { get; set; }
    }
}
