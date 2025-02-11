using Application.Companies.Dtos;

namespace Application.PriceRequests.Dtos
{
    public class GetPriceRequestBriefDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public CompanyBreifDto Company { get; set; }
        public RequestStatus Status { get; set; }
        public string? Notes { get; set; }
    }
}
