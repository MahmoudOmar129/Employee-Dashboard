using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class ResetPasswordVM
    {

        [MinLength(6, ErrorMessage = "min len 6")]
        [Required(ErrorMessage = "password required")]
        public string Password { get; set; }

        [MinLength(6, ErrorMessage = "min len 6")]
        [Required(ErrorMessage = "password confirm required")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }

    }
}
