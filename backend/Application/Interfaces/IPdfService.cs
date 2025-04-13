using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPdfService
    {
        Task<RawUploadResult> AddPdfAsync(IFormFile file);
        Task<string> DeletePdfAsync(string publicId);
    }
}