using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    [Authorize]
    public class NotificationsHub(ICurrentUserService currentUser) : Hub<IClientNotificationMethods>
    {
        public override Task OnConnectedAsync()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"{currentUser.EntityId}-{currentUser.EntityType}");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{currentUser.EntityId} - {currentUser.EntityType}");
        }
    }
}
