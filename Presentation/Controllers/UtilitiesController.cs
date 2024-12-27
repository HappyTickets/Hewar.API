using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    // this controller is only for testing purposes
    [Route("api/utilities")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        [HttpGet("enums")]
        public IActionResult GetSystemEnums(string? name = null)
        {
            var assembly = typeof(Permissions).Assembly;
            var enums = assembly.ExportedTypes
                .Where(e => e.IsEnum)
                .Where(e => name == null || e.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
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
        public async Task<IActionResult> TestNotificationAsync(long userId, AccountTypes accountType, [FromServices] INotificationService notifier)
        {
            await notifier.NotifyUserAsync(userId, accountType, new()
            {
                ContentAr = "اهلا ابراهيم",
                ContentEn = "Hello Ibrahim"
            });
            return Ok();
        }
    }
}
