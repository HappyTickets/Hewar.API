namespace Application.Ads.Dtos.AdServices.Req
{
    public class CreateAdHewarServiceCostDto
    {
        public long ServiceId { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
    }
}