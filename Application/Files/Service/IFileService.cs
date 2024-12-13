using Microsoft.AspNetCore.Http;

namespace Application.Files.Service
{
    public interface IFileService
    {
        Task<Result<MediaDto>> SaveFileAsync(IFormFile file);
    }
}
