using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class BookDetailAndReviewViewModel
    {
        public BookDetailViewModel BookDetailViewModel { get; set; } = new BookDetailViewModel();
        public ReviewViewModel ReviewViewModel { get; set; } = new ReviewViewModel();
        public AddReviewModel AddReviewModel { get; set; } = new AddReviewModel(); 
    }
}
