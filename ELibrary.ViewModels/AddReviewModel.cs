using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELibrary.ViewModels
{
    public class AddReviewModel
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int BookId { get; set; }

      
        public string Subject { get; set; }
       
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser AppUser { get; set; }
    }
}
