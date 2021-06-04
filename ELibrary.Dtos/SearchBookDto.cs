using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Dtos
{
    public class SearchBookDto
    {
        public string SearchTerm { get; set; }
        public string SearchProperty { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
