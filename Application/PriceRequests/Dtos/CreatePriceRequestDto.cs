namespace Application.PriceRequests.Dtos
{
    public class CreatePriceRequestDto
    {
        public SecurityRoles SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public ShiftType WorkShift { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
    }
}
