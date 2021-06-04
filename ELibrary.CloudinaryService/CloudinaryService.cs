using CloudinaryDotNet;
using ELibrary.CloudinaryService.model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ELibrary.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        private static Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration configuration, IOptions<CloudinarySettings> cloudinarySettings)
        {
            Account account = new Account
            {
                Cloud = cloudinarySettings.Value.CloudName,
                ApiKey = cloudinarySettings.Value.ApiKey,
                ApiSecret = cloudinarySettings.Value.ApiSecret,
            };

            _cloudinary = new Cloudinary(account);
        }
    }
}
