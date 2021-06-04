using ELibrary.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Dtos
{
    public class AddBookDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime AddedDate { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public int Copies { get; set; }
        public int AvailableCopies { get; set; }
        public bool Availability { get; set; }
        public int Views { get; set; }
        //public IFormFile PhotoFile { get; set; }
        //public IEnumerable<Rating> Rate { get; set; }
        //public IEnumerable<Review> Reviews { get; set; }
        //public Category Category { get; set; }


    }
}
