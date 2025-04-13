using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services.CloudinaryServices
{
    public class PdfService : IPdfService
    {
        private readonly Cloudinary _cloudinary;
        public PdfService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<RawUploadResult> AddPdfAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }
                return uploadResult;
            }
            throw new ArgumentException("File is empty");
        }
        public async Task<string> DeletePdfAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : throw new Exception("Failed to delete");
        }
    }
}