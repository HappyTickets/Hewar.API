namespace Application.PriceRequests.Dtos
{
    public class CreatePriceRequestDto
    {
        public string SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public string WorkShift { get; set; }
        public string ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
    }
}
