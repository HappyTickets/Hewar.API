using Application.Notifications.Dtos;
using AutoMapper;

namespace Application.Notifications.Mappings
{
    internal class NotificationsProfile: Profile
    {
        public NotificationsProfile()
        {
            CreateMap<Notification, NotificationDto>();
        }
    }
}
