using Application.Files.Dtos;
using Application.Files.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FilesController : ApiControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(FileInfoDTO file)
            => Result(await _fileService.SaveFileAsync(file));
    }
}
