using ELibrary.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELibrary.ViewModels
{
    public class AddBookViewModel
    {
           
          public int Id { get; set; }
         public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Copies { get; set; }
        [Required]
        public int AvailableCopies { get; set; }
        
     

        public IFormFile PhotoFile { get; set; }
        [Required]
        public string Category { get; set; }
        
    }

}
