using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELibrary.Dtos
{
    public class UserBySearchTermDto
    {
        [Required(ErrorMessage ="User Search Term cannot be empty!")]
        public string SearchTerm { get; set; }
        public string SearchProperty { get; set; }
    }
}
