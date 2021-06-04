using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Core.Abstractions
{
    public interface ICloudinaryServices
    {
        public Task<UploadResult> UploadImage(IFormFile image);
    }
}
