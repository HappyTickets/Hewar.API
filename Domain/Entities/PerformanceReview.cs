namespace Domain.Entities
{
    public class PerformanceReview : SoftDeletableEntity
    {
        public DateTimeOffset ReviewDate { get; set; }
        public string ReviewerName { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public int GuardId { get; set; }
        public Guard Guard { get; set; }
    }
}
