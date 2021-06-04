using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Dtos
{
    public class GetBookByCategoryDto
    {
        public int Id { get; set; }
        public string categoryName { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
