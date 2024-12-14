namespace Domain.Events.Notifications
{
    public class NotificationCreated: DomainEvent
    {
        public Notification Notification { get; }

        public NotificationCreated(Notification notification)
        {
            Notification = notification;
        }
    }
}
