using System;

namespace ELibrary.Models
{
    public class Review
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