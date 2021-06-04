using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Dtos
{
   public class AddPhotoDto
    {
        public string id { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
