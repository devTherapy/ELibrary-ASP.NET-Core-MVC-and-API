using System.ComponentModel.DataAnnotations;

namespace ELibrary.Dtos
{
    public class LoginDetailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool RemeberMe { get; set; }
    }
}
