using Application.Files.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/files")]
    public class FilesController : ApiControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile file)
            => Result(await _fileService.SaveFileAsync(file));
    }
}
