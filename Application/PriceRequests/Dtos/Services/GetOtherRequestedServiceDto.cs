namespace Application.PriceRequests.Dtos.Services
{
    public class GetOtherRequestedServiceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
