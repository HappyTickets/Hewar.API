namespace Domain.Entities
{
    public class Shift : SoftDeletableEntity
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public ShiftType ShiftType { get; set; }
        public string Location { get; set; }
        public bool IsTemplate { get; set; }
        public long? ParentShiftId { get; set; }
        public long GuardId { get; set; }
        public Guard Guard { get; set; }
        public Shift ParentShift { get; set; }
    }
}
