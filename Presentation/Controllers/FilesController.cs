using Application.Files.Dtos;
using Application.Files.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FilesController(IFileService fileService) : ApiControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(FileInfoDTO file)
            => Result(await fileService.SaveFileAsync(file));
    }
}
