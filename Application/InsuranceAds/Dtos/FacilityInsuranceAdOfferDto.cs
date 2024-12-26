using Application.Companies.Dtos;

namespace Application.InsuranceAds.Dtos
{
    public class FacilityInsuranceAdOfferDto
    {
        public long Id { get; set; }
        public string Offer { get; set; }
        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long InsuranceAdId { get; set; }

        public CompanyBreifDto Company { get; set; }
    }
}
