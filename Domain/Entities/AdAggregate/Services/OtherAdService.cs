namespace Domain.Entities.AdAggregate.Services
{
    public class OtherAdService
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }

        public long AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
