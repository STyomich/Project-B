using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file);
        Task<string> DeleteFile(string publicId);
        Task<RawUploadResult> AddPdfAsync(IFormFile file);
    }
}