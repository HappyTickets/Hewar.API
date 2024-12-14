namespace Domain.Events.Notifications
{
    public class NotificationRead: DomainEvent
    {
        public Notification Notification { get; }

        public NotificationRead(Notification notification)
        {
            Notification = notification;
        }
    }
}
