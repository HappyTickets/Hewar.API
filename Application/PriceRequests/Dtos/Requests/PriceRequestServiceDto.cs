namespace Application.PriceRequests.Dtos.Requests
{
    public class PriceRequestServiceDto
    {
        public long ServiceId { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }

    }
}