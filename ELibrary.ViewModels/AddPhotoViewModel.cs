using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
   public  class AddPhotoViewModel
    {
      
        public IFormFile PhotoFile { get; set; }
    }
}
