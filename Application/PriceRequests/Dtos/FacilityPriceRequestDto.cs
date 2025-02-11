using Application.Companies.Dtos;
using Application.PriceRequests.Dtos.Services;

namespace Application.PriceRequests.Dtos
{
    public class FacilityPriceRequestDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }

        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Notes { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public ICollection<GetServiceRequestDto> Services { get; set; }
        public ICollection<GetOtherRequestedServiceDto>? OtherServices { get; set; }

        public bool HasOffers { get; set; }
        public CompanyBreifDto Company { get; set; }

    }
}