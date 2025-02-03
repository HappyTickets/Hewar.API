namespace Domain.Entities.PriceRequestAggregates
{
    public class OtherRequestedService
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
