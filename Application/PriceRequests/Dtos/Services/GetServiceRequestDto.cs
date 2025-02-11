namespace Application.PriceRequests.Dtos.Services
{
    public class GetServiceRequestDto
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }

    }
}