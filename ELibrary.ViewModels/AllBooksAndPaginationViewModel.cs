using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class AllBooksAndPaginationViewModel
    {
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public int PageIndex { get; set; }
        public List<AllBooksViewModel> AllBooksViewModel { get; set; } = new List<AllBooksViewModel>();
    }
}
