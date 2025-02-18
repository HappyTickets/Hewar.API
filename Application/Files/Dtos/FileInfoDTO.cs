using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Files.Dtos
{
    public class FileInfoDTO
    {
        [Required]
        public string Path { get; set; } = string.Empty;

        public byte[]? Base64 { get; set; }
        public string? Base64EncodedString { get; set; }

        // Use IFormFile for file uploads
        public IFormFile? FileData { get; set; }
    }

}
