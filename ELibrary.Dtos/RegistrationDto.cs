using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ELibrary.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }
        public IFormFile PhotoFile { get; set; }
        
    }
}
