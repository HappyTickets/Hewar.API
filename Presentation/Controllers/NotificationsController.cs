using Application.Notifications.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace API.Controllers
{
    [Authorize]
    public class NotificationsController : ApiControllerBase
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _notificationsService.GetAllAsync());

        [HttpGet("countUnRead")]
        public async Task<IActionResult> CountUnReadAsync()
            => Result(await _notificationsService.CountUnReadAsync());

        [HttpPatch("markAsRead")]
        public async Task<IActionResult> MarkAsReadAsync(long id)
            => Result(await _notificationsService.MarkAsReadAsync(id));
    }
}
