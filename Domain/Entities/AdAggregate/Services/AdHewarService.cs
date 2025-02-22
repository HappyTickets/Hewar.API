using Domain.Entities.Hewar;

namespace Domain.Entities.AdAggregate.Services
{
    public class AdHewarService
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }

        public long ServiceId { get; set; }
        public virtual HewarService Service { get; set; }

        public long AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
