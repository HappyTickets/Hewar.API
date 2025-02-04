namespace Application.PriceRequests.Dtos.Services
{
    public class ServiceRequestDto
    {
        public long ServiceId { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }

    }
}