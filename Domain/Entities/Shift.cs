
namespace Domain.Entities
{
    public class Shift : SoftDeletableEntity
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string ShiftType { get; set; }
        public string Location { get; set; }
        public bool IsTemplate { get; set; }
        public int? ParentShiftId { get; set; }
        public int GuardId { get; set; }
        public Guard Guard { get; set; }

        // nav props
        public Shift ParentShift { get; set; }
    }
}
