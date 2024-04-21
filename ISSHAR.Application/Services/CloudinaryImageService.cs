using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<IImageService> _logger;

        public CloudinaryImageService(Cloudinary cloudinary , ILogger<IImageService> logger)
        {
            _cloudinary = cloudinary;
            _logger= logger;
        }
        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return null;
            }
            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream())
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult.SecureUri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the image.");
                return null;
            }
        }

    }
}
