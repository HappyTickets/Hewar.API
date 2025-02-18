using Application.Files.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MimeDetective;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Files.Service
{
    public class FileService(IMapper mapper, IWebHostEnvironment webHostEnvironment) : IFileService
    {
        public async Task<Result<MediaDto>> SaveFileAsync(FileInfoDTO file)
        {
            file.Path = file.Path.Replace(" ", "_");

            var mediaDto = new MediaDto();

            if (file.FileData != null)
            {
                file.Path = AdjustPathByCategory(file.FileData.ContentType, file.Path);
                mediaDto.Url = await ConvertStreamToFileAsync(file.FileData, file.Path);
                mediaDto.Type = DetermineFileCategory(file.FileData.ContentType);
            }
            else if (file.Base64 != null)
            {
                file.Path = AdjustPathByCategory("application/octet-stream", file.Path);
                mediaDto.Url = await ConvertBase64ToFileAsync(file.Base64, file.Path);
            }
            else if (file.Base64EncodedString != null)
            {
                file.Path = AdjustPathByCategory("application/octet-stream", file.Path);
                mediaDto.Url = await ConvertBase64ToFileAsync(file.Base64EncodedString, file.Path);
            }

            return Result<MediaDto>.Success(mediaDto, SuccessCodes.fileUploaded);
        }

        private string AdjustPathByCategory(string contentType, string originalPath)
        {
            string baseDirectory = "/files/";

            if (string.IsNullOrEmpty(contentType))
            {
                return Path.Combine(baseDirectory, originalPath);
            }

            if (contentType.StartsWith("image/"))
            {
                baseDirectory = "/images/";
            }
            else if (contentType == "application/pdf" ||
                     contentType.StartsWith("application/msword") ||
                     contentType.StartsWith("application/vnd"))
            {
                baseDirectory = "/files/";
            }

            return Path.Combine(baseDirectory, originalPath);
        }


        private string DetermineFileCategory(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) return "unknown";

            if (contentType.StartsWith("image/")) return "image";
            if (contentType.StartsWith("video/")) return "video";
            if (contentType.StartsWith("audio/")) return "audio";
            if (contentType == "application/pdf" || contentType.StartsWith("application/msword") || contentType.StartsWith("application/vnd"))
            {
                return "document";
            }

            return "unknown";
        }

        private async Task<string> ConvertStreamTobase64Async(IBrowserFile file)
        {
            using var stream = file.OpenReadStream(10000000);
            return await ConvertStreamTobase64Async(stream, file.ContentType);
        }

        private async Task<string> ConvertStreamTobase64Async(Stream stream, string type = "image/jpeg")
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(bytes);
            return $"data:{type};base64,{base64String}";
        }

        private async Task<string> ConvertBase64ToFileAsync(string base64encodedstring, string path)
        {
            base64encodedstring = base64encodedstring.Split(',', 2)[1];
            return await ConvertBase64ToFileAsync(Convert.FromBase64String(base64encodedstring), path);
        }

        private async Task<string> ConvertBase64ToFileAsync(byte[] base64encoded, string path)
        {
            using Stream stream = new MemoryStream(base64encoded);
            return await ConvertStreamToFileAsync(stream, path);
        }

        private async Task<string> ConvertStreamToFileAsync(IFormFile file, string path)
        {
            using var stream = file.OpenReadStream();
            return await ConvertStreamToFileAsync(stream, Path.Combine(path, file.FileName));
        }
        private async Task<string> ConvertStreamToFileAsync(Stream stream, string path)
        {
            try
            {
                // Sanitize the file name and path
                path = SanitizePath(path);

                if (string.IsNullOrWhiteSpace(Path.GetExtension(path)) || string.IsNullOrWhiteSpace(Path.GetFileNameWithoutExtension(path)))
                {
                    var inspector = new ContentInspectorBuilder() { Definitions = MimeDetective.Definitions.Default.All() }.Build();
                    var ext = inspector.Inspect(stream).ByFileExtension().FirstOrDefault()?.Extension;
                    stream.Seek(0, SeekOrigin.Begin);
                    path = $"{path}{Guid.NewGuid()}.{ext}";
                }

                if (string.IsNullOrEmpty(path)) return string.Empty;
                var pathWithoutFileName = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(pathWithoutFileName)) return string.Empty;

                var wwwrootPath = webHostEnvironment.WebRootPath;
                StringBuilder fullPath = new(wwwrootPath);
                fullPath.Append(pathWithoutFileName);

                Directory.CreateDirectory(fullPath.ToString());

                StringBuilder fileName = new();
                fileName.AppendFormat("{0}-{1}{2}".Normalize(), Path.GetFileNameWithoutExtension(path), Guid.NewGuid().ToString(), Path.GetExtension(path));

                string filePath = Path.Combine(fullPath.ToString(), fileName.ToString());

                using var fileStream = File.Create(filePath);
                await stream.CopyToAsync(fileStream);

                return filePath.Replace(webHostEnvironment.WebRootPath, string.Empty);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private string SanitizePath(string path)
        {
            string directoryName = Path.GetDirectoryName(path).Replace(" ", "");
            string fileName = Path.GetFileName(path).Replace(" ", "");
            fileName = Regex.Replace(fileName, @"[^a-zA-Z0-9-_\.]", "");

            return Path.Combine(directoryName, fileName);
        }


    }
}
