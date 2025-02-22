namespace Application.Ads.Dtos.AdServices.Res
{
    public class GetAdHewarServiceCostDto
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}