namespace Domain.Entities.MissionAggregate
{
    public class MissionGuard
    {
        public long Id { get; set; }
        public long MissionId { get; set; }
        public virtual Mission Mission { get; set; }
        public long GuardId { get; set; }
        public virtual Guard Guard { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
