using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public string SearchProperty { get; set; }
        public int PageIndex { get; set; } = 1;
    }
}
