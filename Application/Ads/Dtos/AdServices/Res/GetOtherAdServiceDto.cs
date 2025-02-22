namespace Application.Ads.Dtos.AdServices.Res
{
    public class GetOtherAdServiceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
