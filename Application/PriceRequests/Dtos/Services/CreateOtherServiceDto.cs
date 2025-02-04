namespace Application.PriceRequests.Dtos.Services
{
    public class CreateOtherServiceDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}