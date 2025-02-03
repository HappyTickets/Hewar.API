using Application.Facilities.Dtos;
using Application.PriceRequests.Dtos.Offers;
using Application.PriceRequests.Dtos.Requests.Services;

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

        public ICollection<ServiceRequestDto> Services { get; set; }
        public ICollection<OtherRequestedServiceDto>? OtherServices { get; set; }

        public GetPriceOfferDto Offer { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}