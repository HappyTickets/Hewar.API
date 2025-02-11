using Application.Companies.Dtos;
using Application.Facilities.Dtos;
using Application.PriceRequests.Dtos.Services;

namespace Application.PriceRequests.Dtos
{
    public class GetPriceRequestDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public RequestStatus Status { get; set; }
        public ICollection<CreateServiceRequestDto>? Services { get; set; }
        public ICollection<GetOtherRequestedServiceDto>? OtherServices { get; set; }
        public FacilityBreifDto Facility { get; set; }
        public CompanyBreifDto Company { get; set; }

        public string? Notes { get; set; }
        public bool HasOffers { get; set; }
    }
}
