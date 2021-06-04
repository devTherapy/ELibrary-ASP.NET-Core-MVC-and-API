namespace ELibrary.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Name of Book";
        public string Author { get; set; } = "Name of author";
        public bool Availability { get; set; } = true;
        public string PhotoUrl { get; set; } = "https://dummyimage.com/175x260/287edb/fff.jpg";
        public int Rating { get; set; } = 5;
    }
}