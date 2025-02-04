namespace Application.PriceRequests.Dtos.Services
{
    public class OtherRequestedServiceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
