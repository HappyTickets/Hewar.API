namespace Application.Ads.Dtos.AdServices.Req
{
    public class CreateOtherAdServiceDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
