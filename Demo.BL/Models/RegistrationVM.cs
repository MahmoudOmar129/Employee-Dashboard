using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class RegistrationVM
    {

        [EmailAddress(ErrorMessage = "invalid email")]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "min len 6")]
        [Required(ErrorMessage = "password required")]
        public string Password { get; set; }

        [MinLength(6, ErrorMessage = "min len 6")]
        [Required(ErrorMessage = "password confirm required")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "are you agree ?")]
        public bool IsAgree { get; set; }

    }
}
