using Application.Files.Dtos;

namespace Application.Files.Service
{
    public interface IFileService
    {
        Task<Result<MediaDto>> SaveFileAsync(FileInfoDTO file);
    }
}
