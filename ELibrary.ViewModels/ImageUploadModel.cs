using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    class ImageUploadModel
    {
        public IFormFile PhotoFile { get; set; }
        public string PhotoUrl { get; set; }
    }
}
