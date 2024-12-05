using Application.Common.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Result<TData>(Result<TData> result)
           => result.Status switch
           {
               StatusCodes.Status204NoContent => NoContent(),
               _ => StatusCode(result.Status, result)
           };
    }
}
