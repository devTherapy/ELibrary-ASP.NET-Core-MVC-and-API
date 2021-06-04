namespace ELibrary.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int BookId { get; set; }
        public int Rate { get; set; }
        public Book Book { get; set; }
    }
}
