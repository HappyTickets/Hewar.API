using Application.Common.Interfaces.Services;
using Application.Notifications.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    // this controller is only for testing purposes
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        [HttpGet("enums")]
        public IActionResult GetSystemEnums(string? name = null)
        {
            var assembly = typeof(Permissions).Assembly;
            var enums = assembly.ExportedTypes
                .Where(e => e.IsEnum)
                .Where(e => name == null || e.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => new
                {
                    Name = e.Name,
                    Values = e.GetEnumValues()
                        .Cast<Enum>()
                        .Select(e => new { Text = e.ToString(), Value = Convert.ToInt32(e) })
                });
            return Ok(enums);
        }

        [HttpPost("notify")]
        public async Task<IActionResult> TestNotificationAsync(long userId, EntityTypes entitesType, [FromServices] INotificationService notifier)
        {
            await notifier.NotifyUserAsync(userId, entitesType, new NotificationDto()
            {
                ContentAr = "اررررررحب باليوزر",
                ContentEn = "Hello user"
            });
            return Ok();
        }
        [HttpPost("notifyAll")]

        public async Task<IActionResult> TestNotificationUsersAsync([FromServices] INotificationService notifier)
        {
            await notifier.NotifyAllUsersAsync(new NotificationDto()
            {
                ContentAr = "اررررررحب باليوزر",
                ContentEn = "Hello user"
            });
            return Ok();
        }
    }
}
