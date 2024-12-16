using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    [Authorize]
    public class NotificationsHub: Hub<IClientNotificationMethods>
    {
        private readonly ICurrentUserService _currentUser;

        public NotificationsHub(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public override Task OnConnectedAsync()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"{_currentUser.Id}-{_currentUser.Type}");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{_currentUser.Id}-{_currentUser.Type}");
        }
    }
}
