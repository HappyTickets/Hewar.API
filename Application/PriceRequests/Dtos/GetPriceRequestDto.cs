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
        public string FacilityName { get; set; }
        public string CompanyName { get; set; }
        public string? Notes { get; set; }
        public bool HasOffers { get; set; }
    }
}
