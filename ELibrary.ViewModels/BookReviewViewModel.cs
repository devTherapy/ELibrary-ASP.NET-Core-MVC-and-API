using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
   public  class BookReviewViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
