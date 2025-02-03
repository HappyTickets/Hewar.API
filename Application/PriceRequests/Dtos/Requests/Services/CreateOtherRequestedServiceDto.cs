namespace Application.PriceRequests.Dtos.Requests.Services
{
    public class CreateOtherRequestedServiceDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}