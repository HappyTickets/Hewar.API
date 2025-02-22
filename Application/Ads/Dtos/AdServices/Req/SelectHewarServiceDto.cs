namespace Application.Ads.Dtos.AdServices.Req
{
    public class SelectHewarServiceDto
    {
        public long ServiceId { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
