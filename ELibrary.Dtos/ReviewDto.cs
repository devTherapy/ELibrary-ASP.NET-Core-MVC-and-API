using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELibrary.Dtos
{
    public class ReviewDto
    {
        [Required (ErrorMessage ="The Book Title cannot be empty")]
        public int BookId { get; set; }
        public string Subject { get; set; }

        [Required(ErrorMessage = "The body of the message cannot be empty")]
        public string Body { get; set; }

        public string  AppUserId { get; set; }
    }
}
