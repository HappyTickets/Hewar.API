using Application.Companies.Dtos;

namespace Application.Ads.Dtos
{
    public class FacilityAdOfferDto
    {
        public long Id { get; set; }
        public string Offer { get; set; }
        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long InsuranceAdId { get; set; }

        public CompanyBreifDto Company { get; set; }
    }
}
