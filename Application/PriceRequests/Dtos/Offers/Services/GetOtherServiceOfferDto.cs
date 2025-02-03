namespace Application.PriceRequests.Dtos.Offers.Services
{
    public class GetOtherServiceOfferDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
    }
}
