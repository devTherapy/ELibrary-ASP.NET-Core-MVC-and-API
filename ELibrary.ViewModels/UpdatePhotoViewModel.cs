using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class UpdatePhotoViewModel
    {
        public int id { get; set; }
        public IFormFile PhotoFile;
    }
}
