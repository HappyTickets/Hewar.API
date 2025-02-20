namespace Application.Notifications.Dtos
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public string ContentAr { get; set; }
        public string ContentEn { get; set; }
        public bool IsRead { get; set; }
        public long ReferenceId { get; set; }
        public ReferenceTypes ReferenceType { get; set; }
        public NotificationEvents Event { get; set; }
        public DateTimeOffset NotifiedOn { get; set; }
    }
}
