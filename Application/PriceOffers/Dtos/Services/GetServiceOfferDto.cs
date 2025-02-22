namespace Application.PriceOffers.Dtos.Services
{
    public class GetServiceCostDto
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}