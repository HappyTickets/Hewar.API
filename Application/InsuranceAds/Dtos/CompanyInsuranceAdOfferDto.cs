namespace Application.InsuranceAds.Dtos
{
    public class CompanyInsuranceAdOfferDto
    {
        public long Id { get; set; }
        public string Offer { get; set; }
        public RequestStatus Status { get; set; }
        public long InsuranceAdOfferId { get; set; }
    }
}
