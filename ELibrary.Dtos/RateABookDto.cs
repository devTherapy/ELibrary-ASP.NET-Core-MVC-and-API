using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Dtos
{
    public class RateABookDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int RatingValue { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
