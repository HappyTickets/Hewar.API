namespace Application.Notifications.Dtos
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public string ContentAr { get; set; }
        public string ContentEn { get; set; }
        public bool IsRead { get; set; }
        public long ReferenceId { get; set; }
        public string ReferenceType { get; set; }
        public string Event { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
