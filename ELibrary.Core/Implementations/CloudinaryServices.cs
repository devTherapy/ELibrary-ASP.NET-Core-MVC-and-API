using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ELibrary.Core.Abstractions;
using ELibrary.Models.AppsettingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELibrary.Core.Implementations
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly  Cloudinary _cloudinary;
        private readonly PhotoSettings _photoSettings;
        private readonly IConfiguration _appConfig;

        public CloudinaryServices(IConfiguration configuration, IOptions<CloudinarySettings> cloudinarySettings, IOptions<PhotoSettings> photoSettings)
        {
            Account account = new Account
            {
                Cloud = cloudinarySettings.Value.CloudName,
                ApiKey = cloudinarySettings.Value.ApiKey,
                ApiSecret = cloudinarySettings.Value.ApiSecret,
            };

            _cloudinary = new Cloudinary(account);
            _photoSettings = photoSettings.Value;
            _appConfig = configuration;
        }






        public async Task<UploadResult> UploadImage(IFormFile image)
        {
            // validate the image size and extension type using settings from appsettings
            var pictureFormat = false;
            var listOfExtensions = _appConfig.GetSection("PhotoSettings:Extensions").Get<List<string>>();

            for (int i = 0; i < listOfExtensions.Count; i++)
            {
                if (image.FileName.EndsWith(listOfExtensions[i]))
                {
                    pictureFormat = true;
                    break;
                }
            }
            if (pictureFormat == false)
                throw new Exception("File must be .jpg, .jpeg or .png");



            var pixSize = Convert.ToInt64(_photoSettings.Size) * 1048576;
            if (image == null || image.Length > pixSize)
                throw new Exception("File size should not exceed 2mb");
            if (!pictureFormat)
                throw new Exception("File format is not supported. Please upload a picture");



            //object to return
            var uploadResult = new ImageUploadResult();



            //fetch image as stream of data
            using (var imageStream = image.OpenReadStream())
            {
                string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                //upload to cloudinary
                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(fileName, imageStream),
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(300)
                                                        .Height(300)
                });
            }

            /*            var imageUploadResult = new ImageUploadResult();

                        using (var fs = image.OpenReadStream())
                        {
                            var imageUploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(image.FileName, fs),
                                Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                            };
                            imageUploadResult = _cloudinary.Upload(imageUploadParams);
                        }
            */

            return uploadResult;
        }
    }
}
