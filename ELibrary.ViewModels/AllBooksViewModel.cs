using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class AllBooksViewModel
    {

        public int Id { get; set; }

        public string PhotoUrl { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }

    }
}
