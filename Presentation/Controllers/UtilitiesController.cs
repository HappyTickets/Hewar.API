using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/utilities")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        [HttpGet("enums")]
        public IActionResult GetSystemEnums()
        {
            var assembly = typeof(Permissions).Assembly;
            var enums = assembly.ExportedTypes
                .Where(e => e.IsEnum)
                .Select(e => new
                {
                    Name = e.Name,
                    Values = e.GetEnumValues()
                        .Cast<Enum>()
                        .Select(e => new { Text = e.ToString(), Value = Convert.ToInt32(e) })
                });
            return Ok(enums);
        }
    }
}
