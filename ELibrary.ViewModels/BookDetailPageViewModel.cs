using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class BookDetailPageViewModel
    {
        public BookDetailPageViewModel BookDetail { get; set; }
        public IEnumerable<BookReviewViewModel> BookReview { get; set; }
        public IEnumerable<RateViewModel> Rating { get; set; }
    }
}
