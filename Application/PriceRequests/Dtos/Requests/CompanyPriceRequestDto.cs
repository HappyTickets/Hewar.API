using Application.Facilities.Dtos;
using Application.PriceRequests.Dtos.Offers;

namespace Application.PriceRequests.Dtos.Requests
{
    public class CompanyPriceRequestDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Notes { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public PriceOfferDto Offer { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}