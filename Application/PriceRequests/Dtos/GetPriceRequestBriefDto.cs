namespace Application.PriceRequests.Dtos
{
    public class GetPriceRequestBriefDto
    {
        public long Id { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? Notes { get; set; }
    }
}
