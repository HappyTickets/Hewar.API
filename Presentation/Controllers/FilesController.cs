using Application.Files.Dtos;
using Application.Files.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FilesController(IFileService fileService) : ApiControllerBase
    {
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAsync([FromForm] FileInfoDTO file)
     => Result(await fileService.SaveFileAsync(file));

    }
}
