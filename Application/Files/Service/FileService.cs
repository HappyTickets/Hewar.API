using Application.Files.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Service
{
    internal class FileService: IFileService
    {
        private readonly IMapper _mapper;

        public FileService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Result<MediaDto>> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fileType = file.ContentType.Substring(0, file.ContentType.IndexOf('/'));
            var relativeFilePath = Path.Combine("files", fileType + "s", fileName);
            var physicalFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeFilePath);

            var directoryPath = Path.GetDirectoryName(physicalFilePath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath!);

            using var fileStream = File.Create(physicalFilePath);
            await file.CopyToAsync(fileStream);

            return new MediaDto
            {
                Type = fileType,
                Url = relativeFilePath
            };
        }
    }
}
