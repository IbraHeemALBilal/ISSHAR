using Microsoft.AspNetCore.Http;

namespace ISSHAR.Application.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
