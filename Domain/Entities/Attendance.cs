namespace Domain.Entities
{
    public class Attendance : SoftDeletableEntity
    {
        public DateTimeOffset CheckInTime { get; set; }
        public DateTimeOffset? CheckOutTime { get; set; }
        public long GuardId { get; set; }
        public Guard Guard { get; set; }
    }
}
